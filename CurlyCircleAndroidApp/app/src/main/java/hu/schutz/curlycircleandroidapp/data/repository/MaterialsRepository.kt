package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Material
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface MaterialsRepository {

    fun getMaterialsStream(): Flow<Result<List<Material>>>

    suspend fun getMaterials(forceUpdate: Boolean = false): Result<List<Material>>

    fun getMaterialStream(id: Int): Flow<Result<Material>>

    suspend fun getMaterial(id: Int, forceUpdate: Boolean = false): Result<Material>
}