package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Update
import hu.schutz.curlycircleandroidapp.data.User
import kotlinx.coroutines.flow.Flow

interface UserDao {

    @Query("SELECT * FROM user")
    fun getUserStream(): Flow<User>

    @Query("SELECT * FROM user")
    fun getUser(): User

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertUser(user: User)

    @Query("DELETE FROM user")
    suspend fun deleteUser()

    @Update
    suspend fun updateUser(user: User)
}