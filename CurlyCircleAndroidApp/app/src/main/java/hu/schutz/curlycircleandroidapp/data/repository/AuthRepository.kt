package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*

interface AuthRepository {

    fun getAccessToken(): Result<String?>

    fun getRefreshToken(): Result<String?>

    suspend fun setTokens(tokenViewModel: TokenViewModel)

    suspend fun login(loginDto: LoginDto): Result<User>

    suspend fun logout()

    suspend fun register(registerDto: RegisterDto): Result<EntityCreatedViewModel>

    suspend fun refreshToken(): Result<String?>
}