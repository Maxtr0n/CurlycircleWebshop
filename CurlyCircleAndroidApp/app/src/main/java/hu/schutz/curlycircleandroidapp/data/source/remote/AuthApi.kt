package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.*
import retrofit2.http.Body
import retrofit2.http.POST

interface AuthApi {

    companion object {
        const val BASE_URL = "https://curlycircleapi.azurewebsites.net/api/"
    }

    @POST("auth/login")
    suspend fun login(@Body loginDto: LoginDto): UserViewModel

    @POST("auth/register")
    suspend fun register(@Body registerDto: RegisterDto): EntityCreatedViewModel

    @POST("auth/refresh")
    suspend fun refreshToken(@Body refreshDto: RefreshDto): TokenViewModel
}