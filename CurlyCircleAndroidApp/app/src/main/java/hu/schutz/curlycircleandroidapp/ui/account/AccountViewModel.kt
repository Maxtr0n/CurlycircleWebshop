package hu.schutz.curlycircleandroidapp.ui.account

import android.util.Log
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.LoginDto
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.User
import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import hu.schutz.curlycircleandroidapp.ui.shop.productdetails.ProductDetailsUiState
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

/**
 * UI state for the Account route.
 *
 * This is derived from [AccountViewModelState], but split into two possible subclasses to more
 * precisely represent the state available to render the UI.
 */
sealed interface AccountUiState {
    val isLoading: Boolean
    val userMessage: Int?

    data class HasUser(
        val user: User,
        override val isLoading: Boolean = false,
        override val userMessage: Int? = null
    ) : AccountUiState

    data class NoUser(
        val email: String = "",
        val password: String = "",
        override val isLoading: Boolean = false,
        override val userMessage: Int? = null
    ) : AccountUiState
}

/**
 * An internal representation of the Account route state, in a raw form
 */
private data class AccountViewModelState(
    val user: User? = null,
    val email: String = "",
    val password: String = "",
    val isLoading: Boolean = false,
    val userMessage: Int? = null
) {

    /**
     * Converts this [AccountViewModelState] into a more strongly typed [AccountUiState] for driving
     * the ui.
     */
    fun toUiState(): AccountUiState =
        if (user == null) {
            AccountUiState.NoUser (
                email = email,
                password = password,
                isLoading = isLoading,
                userMessage = userMessage,
            )
        } else {
            AccountUiState.HasUser(
                user = user,
                isLoading = isLoading,
                userMessage = userMessage,
            )
        }
}



@HiltViewModel
class AccountViewModel @Inject constructor(
    private val authRepository: AuthRepository,
    private val userRepository: UserRepository
) : ViewModel() {

    private val viewModelState = MutableStateFlow(AccountViewModelState(isLoading = true))

    // UI state exposed to the UI
    val uiState = viewModelState
        .map { it.toUiState() }
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = viewModelState.value.toUiState()
        )

    init {
        getUser()

        viewModelScope.launch {
            userRepository.getUserStream().collect { result ->
                when(result) {
                    is Result.Success -> {
                        viewModelState.update { it.copy(user = result.data) }
                    }
                    is Result.Error -> {
                        viewModelState.update { it.copy(user = null) }
                    }
                }
            }
        }
    }

    /*
    val uiState: StateFlow<AccountUiState> = combine(
        _userMessage, _isLoading, _userAsync, _email, _password
    ) { userMessage, isLoading, userAsync, email, password ->
        when (userAsync) {
            Async.Loading -> {
                AccountUiState(
                    isLoading = true,
                )
            }
            is Async.Success -> {
                AccountUiState(
                    user = userAsync.data,
                    isLoading = isLoading,
                    userMessage = userMessage
                )
            }
        }
    }
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = AccountUiState(isLoading = true)
        )

    private fun handleResult(userResult: Result<User>): Async<User?> =
        if (userResult is Result.Success) {
            Async.Success(userResult.data)
        } else {
            showSnackBarMessage(R.string.user_loading_error)
            Async.Success(null)
        }
     */


    private fun getUser() {
        viewModelState.update { it.copy(isLoading = true) }

        viewModelScope.launch {
            val result = userRepository.getUser()
            viewModelState.update {
                when(result) {
                    is Result.Success -> it.copy(user = result.data, isLoading = false)
                    is Result.Error -> {
                        it.copy(user = null, isLoading = false)
                    }
                }
            }
        }
    }

    fun updateEmail(newEmail: String) {
        viewModelState.update {
            it.copy(email = newEmail)
        }
    }

    fun updatePassword(newPassword: String) {
        viewModelState.update {
            it.copy(password = newPassword)
        }
    }

    fun login() {
        if (viewModelState.value.email.isEmpty() || viewModelState.value.password.isEmpty()) {
            viewModelState.update {
                it.copy(userMessage = R.string.form_not_valid)
            }
            return
        }

        viewModelState.update { it.copy(isLoading = true) }

        // TODO: cartId-t megadni
        viewModelScope.launch {
            when (authRepository.login(viewModelState.value.email, viewModelState.value.password)) {
                is Result.Success -> {
                    viewModelState.update {
                        it.copy(userMessage = R.string.login_successful, isLoading = false)
                    }
                }
                is Result.Error -> {
                    viewModelState.update {
                        it.copy(userMessage = R.string.login_invalid_credentials, isLoading = false)
                    }
                }
            }
        }
    }

    fun logout() {
        viewModelState.update { it.copy(isLoading = true) }

        viewModelScope.launch {
            authRepository.logout()
            viewModelState.update { it.copy(isLoading = false) }
        }
    }

    fun snackBarMessageShown() {
        viewModelState.update {
            it.copy(userMessage = null)
        }
    }

    fun showRegistrationResultUserMessage(userMessage: Int) {
        viewModelState.update {
            it.copy(userMessage = userMessage)
        }
    }
}

