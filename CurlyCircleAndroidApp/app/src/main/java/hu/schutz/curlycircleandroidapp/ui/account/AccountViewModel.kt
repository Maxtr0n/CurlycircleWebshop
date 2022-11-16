package hu.schutz.curlycircleandroidapp.ui.account

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import javax.inject.Inject


data class AccountUiState(
    val loggedIn: Boolean = false,
    val isLoading: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class AccountViewModel @Inject constructor(
    private val authRepository: AuthRepository,
    private val userRepository: UserRepository
) : ViewModel() {

}

