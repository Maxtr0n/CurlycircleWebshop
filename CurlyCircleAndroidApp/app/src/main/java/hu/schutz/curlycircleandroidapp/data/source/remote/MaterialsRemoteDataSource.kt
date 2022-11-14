package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.Material
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.MaterialsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext

class MaterialsRemoteDataSource(
    private val api: CurlyCircleApi,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : MaterialsDataSource {
    override fun getMaterialsStream(): Flow<Result<List<Material>>> {
        return MutableStateFlow(
            runBlocking {
                getMaterials()
            }
        )
    }

    override suspend fun getMaterials(): Result<List<Material>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getMaterials().materials)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getMaterialStream(id: Int): Flow<Result<Material>> {
        return MutableStateFlow(
            runBlocking {
                getMaterial(id)
            }
        )
    }

    override suspend fun getMaterial(id: Int): Result<Material> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getMaterial(id))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveMaterial(material: Material) {
        // NO-OP
    }

    override suspend fun deleteAllMaterials() {
        // NO-OP
    }

}