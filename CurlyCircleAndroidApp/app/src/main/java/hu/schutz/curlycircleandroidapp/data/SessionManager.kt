package hu.schutz.curlycircleandroidapp.data

import hu.schutz.curlycircleandroidapp.data.repository.AuthRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import kotlinx.coroutines.runBlocking
import javax.inject.Inject

class SessionManager @Inject constructor(
    private val authRepository: AuthRepository
) {

    fun getAccessToken(): String? {
        val token = authRepository.getAccessToken()
        return if (token is Result.Success) {
            token.data
        } else {
            null
        }
    }

    fun getRefreshToken(): String? {
        val token = authRepository.getRefreshToken()
        return if (token is Result.Success) {
            token.data
        } else {
            null
        }
    }

    fun refreshToken(): String? {
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