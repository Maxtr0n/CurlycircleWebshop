package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.Material
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface MaterialsDataSource {

    fun getMaterialsStream(): Flow<Result<List<Material>>>

    suspend fun getMaterials(): Result<List<Material>>

    fun getMaterialStream(id: Int): Flow<Result<Material>>

    suspend fun getMaterial(id: Int): Result<Material>

    suspend fun saveMaterial(material: Material)

    suspend fun deleteAllMaterials()
}