package hu.schutz.curlycircleandroidapp.util

import hu.schutz.curlycircleandroidapp.data.SessionManager
import okhttp3.Interceptor
import okhttp3.Request
import okhttp3.Response

class TokenInterceptor(
    private val sessionManager: SessionManager
) : Interceptor {

    override fun intercept(chain: Interceptor.Chain): Response {
        val request = chain.request()
        val accessToken = sessionManager.getAccessToken()

        return chain.proceed(newRequestWithAccessToken(accessToken, request))
    }

    private fun newRequestWithAccessToken(accessToken: String?, request: Request): Request =
        request.newBuilder()
            .header("Authorization", "Bearer $accessToken")
            .build()
}