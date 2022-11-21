package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.source.local.AppSharedPreferences
import hu.schutz.curlycircleandroidapp.data.source.local.dao.UserDao
import hu.schutz.curlycircleandroidapp.data.source.remote.AuthApi
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext

class DefaultAuthRepository(
    private val api: AuthApi,
    private val dao: UserDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO,
    private val sharedPreferences: AppSharedPreferences
) : AuthRepository {

    override suspend fun getAccessToken(): Result<String?> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getUser().accessToken)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }


    override suspend fun getRefreshToken(): Result<String?> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getUser().refreshToken)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun setTokens(tokenViewModel: TokenViewModel) {
        val user = dao.getUser()
        user.accessToken = tokenViewModel.accessToken
        user.refreshToken = tokenViewModel.refreshToken

        dao.updateUser(user)
    }

    override suspend fun login(email: String, password: String): Result<User> =
        withContext(ioDispatcher) {
            return@withContext try {
                val cartId = sharedPreferences.getCardId()
                val userViewModel = api.login(LoginDto(email, password, if (cartId != 0) cartId else null))

                val user = User(
                    databaseId = 1,
                    id = userViewModel.id,
                    email = userViewModel.email,
                    cartId = userViewModel.cartId,
                    city = userViewModel.city,
                    zipCode = userViewModel.zipCode,
                    line1 = userViewModel.line1,
                    line2 = userViewModel.line2,
                    firstName = userViewModel.firstName,
                    lastName = userViewModel.lastName,
                    role = userViewModel.role,
                    phoneNumber = userViewModel.phoneNumber,
                    accessToken = userViewModel.accessToken,
                    refreshToken = userViewModel.refreshToken
                )

                dao.insertUser(user)

                Result.Success(user)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun logout() {
        dao.deleteUser()
    }

    override suspend fun register(registerDto: RegisterDto): Result<EntityCreatedViewModel>  =
        withContext(ioDispatcher) {
            return@withContext try {
                Result.Success(api.register(registerDto))
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun refreshToken(): Result<String?> = withContext(ioDispatcher) {
        return@withContext try {
            val user = dao.getUser()

            val refreshDto = RefreshDto(
                email = user.email,
                accessToken = user.accessToken,
                refreshToken = user.refreshToken,
                id = user.id
            )

            val tokenViewModel = api.refreshToken(refreshDto)
            setTokens(tokenViewModel)

            Result.Success(tokenViewModel.accessToken)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }
}