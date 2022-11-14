package hu.schutz.curlycircleandroidapp.data.source.local.dao

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import hu.schutz.curlycircleandroidapp.data.Material
import kotlinx.coroutines.flow.Flow

@Dao
interface MaterialsDao {

    @Query("SELECT * FROM materials")
    fun getMaterialsStream(): Flow<List<Material>>

    @Query("SELECT * FROM materials")
    fun getMaterials(): List<Material>

    @Query("SELECT * FROM materials WHERE id = :id")
    fun getMaterialStream(id: Int): Flow<Material>

    @Query("SELECT * FROM materials WHERE id = :id")
    fun getMaterial(id: Int): Material

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertMaterial(material: Material)

    @Query("DELETE FROM materials")
    suspend fun deleteMaterials()
}