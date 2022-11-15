package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Material
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.MaterialsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultMaterialsRepository(
    private val materialsRemoteDataSource: MaterialsDataSource,
    private val materialsLocalDataSource: MaterialsDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : MaterialsRepository {

    override fun getMaterialsStream(): Flow<Result<List<Material>>> {
        return materialsLocalDataSource.getMaterialsStream()
    }

    override suspend fun getMaterials(forceUpdate: Boolean): Result<List<Material>> {
        if (forceUpdate) {
            try {
                updateMaterialsFromRemoteDataSource()
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return materialsLocalDataSource.getMaterials()
    }

    override fun getMaterialStream(id: Int): Flow<Result<Material>> {
        return materialsLocalDataSource.getMaterialStream(id)
    }

    override suspend fun getMaterial(id: Int, forceUpdate: Boolean): Result<Material> {
        if (forceUpdate) {
            try {
                updateMaterialFromRemoteDataSource(id)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return materialsLocalDataSource.getMaterial(id)
    }

    private suspend fun updateMaterialsFromRemoteDataSource() {
        val remoteMaterials = materialsRemoteDataSource.getMaterials()

        if (remoteMaterials is Result.Success) {
            materialsLocalDataSource.deleteAllMaterials()
            remoteMaterials.data.forEach { material ->
                materialsLocalDataSource.saveMaterial(material)
            }
        } else if (remoteMaterials is Result.Error) {
            throw remoteMaterials.exception
        }
    }

    private suspend fun updateMaterialFromRemoteDataSource(id: Int) {
        val remoteMaterial = materialsRemoteDataSource.getMaterial(id)

        if (remoteMaterial is Result.Success) {
            materialsLocalDataSource.saveMaterial(remoteMaterial.data)

        } else if (remoteMaterial is Result.Error) {
            throw remoteMaterial.exception
        }
    }
}