package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import kotlinx.coroutines.flow.Flow

interface UserRepository {

    fun getUserStream(): Flow<Result<User>>

    suspend fun getUser(): Result<User>

    suspend fun logout()

    suspend fun updateUser(userUpdateDto: UserUpdateDto): Result<UserDataViewModel>

    suspend fun changePassword(changePasswordDto: ChangePasswordDto): Result<Unit>
}