package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Color
import kotlinx.coroutines.flow.Flow

@Dao
interface ColorsDao {

    @Query("SELECT * FROM colors")
    fun getColorsStream(): Flow<List<Color>>

    @Query("SELECT * FROM colors")
    suspend fun getColors(): List<Color>

    @Query("SELECT * FROM colors WHERE id IN (:ids)")
    fun getColorsStream(ids: Set<Int>): Flow<List<Color>>

    @Query("SELECT * FROM colors WHERE id IN (:ids)")
    suspend fun getColors(ids: Set<Int>): List<Color>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertColor(color: Color)

    @Query("DELETE FROM colors")
    suspend fun deleteColors()

}