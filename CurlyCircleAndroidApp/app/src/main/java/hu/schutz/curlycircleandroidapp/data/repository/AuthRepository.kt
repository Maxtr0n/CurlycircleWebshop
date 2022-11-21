package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*

interface AuthRepository {

    suspend fun getAccessToken(): Result<String?>

    suspend fun getRefreshToken(): Result<String?>

    suspend fun setTokens(tokenViewModel: TokenViewModel)

    suspend fun login(email: String, password: String): Result<User>

    suspend fun logout()

    suspend fun register(registerDto: RegisterDto): Result<EntityCreatedViewModel>

    suspend fun refreshToken(): Result<String?>
}