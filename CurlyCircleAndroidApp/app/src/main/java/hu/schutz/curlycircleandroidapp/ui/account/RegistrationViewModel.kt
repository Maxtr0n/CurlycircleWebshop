package hu.schutz.curlycircleandroidapp.ui.account

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.RegisterDto
import hu.schutz.curlycircleandroidapp.data.Result.Error
import hu.schutz.curlycircleandroidapp.data.Result.Success
import hu.schutz.curlycircleandroidapp.data.User
import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

data class RegistrationUiState(
    val email: String = "",
    val password: String = "",
    val firstName: String = "",
    val lastName: String = "",
    val city: String = "",
    val zipCode: String = "",
    val line1: String = "",
    val line2: String = "",
    val phoneNumber: String = "",
    val isLoading: Boolean = false,
    val registrationSuccessful: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class RegistrationViewModel @Inject constructor(
    private val authRepository: AuthRepository
) : ViewModel() {

    // A MutableStateFlow needs to be created in this ViewModel. The source of truth of the current
    // editable Task is the ViewModel, we need to mutate the UI state directly in methods such as
    // `updateTitle` or `updateDescription`
    private val _uiState = MutableStateFlow(RegistrationUiState())
    val uiState: StateFlow<RegistrationUiState> = _uiState.asStateFlow()

    fun snackBarMessageShown() {
        _uiState.update {
            it.copy(userMessage = null)
        }
    }

    private fun showSnackBar(message: Int) {
        _uiState.update {
            it.copy(userMessage = message)
        }
    }

    fun updateEmail(newEmail: String) {
        _uiState.update {
            it.copy(email = newEmail)
        }
    }

    fun updatePassword(newPassword: String) {
        _uiState.update {
            it.copy(password = newPassword)
        }
    }

    fun updateFirstName(newFirstName: String) {
        _uiState.update {
            it.copy(firstName = newFirstName)
        }
    }

    fun updateLastName(newLastName: String) {
        _uiState.update {
            it.copy(lastName = newLastName)
        }
    }

    fun updateCity(newCity: String) {
        _uiState.update {
            it.copy(city = newCity)
        }
    }

    fun updateZipCode(newZipCode: String) {
        _uiState.update {
            it.copy(zipCode = newZipCode)
        }
    }

    fun updateLine1(newLine1: String) {
        _uiState.update {
            it.copy(line1 = newLine1)
        }
    }

    fun updateLine2(newLine2: String) {
        _uiState.update {
            it.copy(line2 = newLine2)
        }
    }

    fun updatePhoneNumber(newPhoneNumber: String) {
        _uiState.update {
            it.copy(phoneNumber = newPhoneNumber)
        }
    }

    fun register() {

        if (uiState.value.email.isEmpty() || uiState.value.password.isEmpty() ||
            uiState.value.firstName.isEmpty() || uiState.value.lastName.isEmpty() ||
            uiState.value.city.isEmpty() || uiState.value.zipCode.isEmpty() ||
            uiState.value.line1.isEmpty() || uiState.value.phoneNumber.isEmpty()) {
            _uiState.update {
                it.copy(userMessage = R.string.form_not_valid)
            }
            return
        }

        _uiState.update {
            it.copy(isLoading = true)
        }

        viewModelScope.launch {
            val registerDto = RegisterDto(
                email = uiState.value.email,
                password = uiState.value.password,
                firstName = uiState.value.firstName,
                lastName = uiState.value.lastName,
                city = uiState.value.city,
                zipCode = uiState.value.zipCode,
                line1 = uiState.value.line1,
                line2 = uiState.value.line2,
                phoneNumber = uiState.value.phoneNumber
            )

            when (authRepository.register(registerDto)) {
                is Success -> {

                    //navigáljunk át az account screenre
                    _uiState.update {
                        it.copy(registrationSuccessful = true, isLoading = false)
                    }
                }
                is Error -> {
                    _uiState.update {
                        it.copy(userMessage = R.string.registration_failed, isLoading = false)
                    }
                }
            }
        }
    }
}