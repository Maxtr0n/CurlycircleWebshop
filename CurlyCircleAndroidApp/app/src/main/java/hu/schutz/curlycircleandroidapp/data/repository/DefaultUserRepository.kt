package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.local.dao.UserDao
import hu.schutz.curlycircleandroidapp.data.source.remote.CurlyCircleApi
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext


// The user repository interacts directly with the CurlyCircleApi and UserDao, instead of
// having local and remote DataSource classes, since the authentication and user data
// handling is a more complex scenario, therefore it did not make sense to try to
// make a transparent DataSource interface and DataSource classes that implement it
class DefaultUserRepository(
    private val api: CurlyCircleApi,
    private val dao: UserDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : UserRepository {

    override fun getUserStream(): Flow<Result<User?>> {
       return dao.getUserStream().map {
           Result.Success(it)
       }
    }

    override suspend fun getUser(): Result<User?> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getUser())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    /*
    override suspend fun updateUser(userUpdateDto: UserUpdateDto): Result<UserDataViewModel> =
        withContext(ioDispatcher) {
            return@withContext try {
                val userData = api.updateUser(userUpdateDto)

                val user = dao.getUser()

                user.email = userData.email
                user.firstName = userData.firstName
                user.lastName = userData.lastName
                user.city = userData.city
                user.zipCode= userData.zipCode
                user.line1 = userData.line1
                user.line2= userData.line2
                user.phoneNumber= userData.phoneNumber

                dao.updateUser(user)

                Result.Success(userData)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun changePassword(changePasswordDto: ChangePasswordDto): Result<Unit> =
        withContext(ioDispatcher) {
            return@withContext try {
                Result.Success(api.changePassword(changePasswordDto))
            } catch (e: Exception) {
                Result.Error(e)
            }
        }
     */
}