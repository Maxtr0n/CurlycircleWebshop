package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.*
import kotlinx.coroutines.flow.Flow
import kotlin.Result

interface UserDataSource {

    fun getUserStream(): Flow<Result<User>>

    suspend fun getUser(): Result<User>

    suspend fun saveUser(user: User)

    suspend fun deleteUser()

    suspend fun login(loginDto: LoginDto): UserViewModel

    suspend fun register(registerDto: RegisterDto): EntityCreatedViewModel

    suspend fun refreshToken(refreshDto: RefreshDto): TokenViewModel

    suspend fun updateUser(userUpdateDto: UserUpdateDto): UserDataViewModel

    suspend fun changePassword(changePasswordDto: ChangePasswordDto)
}