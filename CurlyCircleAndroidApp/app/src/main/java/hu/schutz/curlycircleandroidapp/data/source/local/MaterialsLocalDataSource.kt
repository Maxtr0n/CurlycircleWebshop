package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.Material
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.MaterialsDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.MaterialsDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class MaterialsLocalDataSource internal constructor(
    private val dao: MaterialsDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : MaterialsDataSource {

    override fun getMaterialsStream(): Flow<Result<List<Material>>> {
        return dao.getMaterialsStream().map {
            Result.Success(it)
        }
    }

    override suspend fun getMaterials(): Result<List<Material>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getMaterials())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getMaterialStream(id: Int): Flow<Result<Material>> {
        return dao.getMaterialStream(id).map {
            Result.Success(it)
        }
    }

    override suspend fun getMaterial(id: Int): Result<Material> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getMaterial(id))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveMaterial(material: Material) {
        dao.insertMaterial(material)
    }

    override suspend fun deleteAllMaterials() {
        dao.deleteMaterials()
    }

}