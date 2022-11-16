package hu.schutz.curlycircleandroidapp.util

import hu.schutz.curlycircleandroidapp.data.SessionManager
import hu.schutz.curlycircleandroidapp.data.TokenViewModel
import kotlinx.coroutines.runBlocking
import okhttp3.Authenticator
import okhttp3.Request
import okhttp3.Response
import okhttp3.Route

class TokenAuthenticator(
    private val sessionManager: SessionManager
) : Authenticator {

    override fun authenticate(route: Route?, response: Response): Request? {
        return runBlocking {
            when (val accessToken = getUpdatedToken()) {
                null -> null
                else -> {
                    response.request.newBuilder()
                        .header("Authorization", "Bearer $accessToken")
                        .build()
                }
            }
        }
    }

    private suspend fun getUpdatedToken(): String? {
        val refreshToken = sessionManager.getRefreshToken()
        refreshToken?.let {
            return sessionManager.refreshToken()
        } ?: return null
    }

}