package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Pattern
import kotlinx.coroutines.flow.Flow

@Dao
interface PatternsDao {
    @Query("SELECT * FROM patterns")
    fun getPatternsStream(): Flow<List<Pattern>>

    @Query("SELECT * FROM patterns")
    fun getPatterns(): List<Pattern>

    @Query("SELECT * FROM patterns WHERE id = :id")
    fun getPatternStream(id: Int): Flow<Pattern>

    @Query("SELECT * FROM patterns WHERE id = :id")
    fun getPattern(id: Int): Pattern

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertPattern(pattern: Pattern)

    @Query("DELETE FROM patterns")
    suspend fun deletePatterns()
}