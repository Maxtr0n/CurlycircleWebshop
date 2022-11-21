package hu.schutz.curlycircleandroidapp.data

import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import kotlinx.coroutines.runBlocking
import javax.inject.Inject

class SessionManager @Inject constructor(
    private val authRepository: AuthRepository
) {

    fun getAccessToken(): String? {
        return runBlocking {
            val token = authRepository.getAccessToken()
            if (token is Result.Success) {
                token.data
            } else {
                null
            }
        }
    }

    fun getRefreshToken(): String? {
        return runBlocking {
            val token = authRepository.getRefreshToken()
            if (token is Result.Success) {
                token.data
            } else {
                null
            }
        }
    }

    fun refreshToken(): Result<String?> {
        return runBlocking {
            authRepository.refreshToken()
        }
    }

    fun logout() {
        runBlocking {
            authRepository.logout()
        }
    }
}