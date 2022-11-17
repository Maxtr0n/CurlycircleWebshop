package hu.schutz.curlycircleandroidapp.ui.account

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.User
import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import hu.schutz.curlycircleandroidapp.ui.shop.productdetails.ProductDetailsUiState
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import javax.inject.Inject


data class AccountUiState(
    val user: User? = null,
    val email: String = "",
    val password: String = "",
    val isLoading: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class AccountViewModel @Inject constructor(
    private val authRepository: AuthRepository,
    private val userRepository: UserRepository
) : ViewModel() {

    private val _userMessage: MutableStateFlow<Int?> = MutableStateFlow(null)
    private  val _isLoading = MutableStateFlow(false)
    private val _userAsync = userRepository.getUserStream()
        .map { handleResult(it) }
        .onStart { emit(Async.Loading) }

    val uiState: StateFlow<AccountUiState> = combine(
        _userMessage, _isLoading, _userAsync
    ) { userMessage, isLoading, userAsync ->
        when (userAsync) {
            Async.Loading -> {
                AccountUiState(isLoading = true)
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

    fun login() {

    }

    fun logout() {

    }

    fun snackBarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackBarMessage(message: Int) {
        _userMessage.value = message
    }


}

