package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.Color
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ColorsDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.ColorsDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class ColorsLocalDataSource internal constructor(
    private val dao: ColorsDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ColorsDataSource {

    override fun getColorsStream(): Flow<Result<List<Color>>> {
        return dao.getColorsStream().map {
            Result.Success(it)
        }
    }

    override suspend fun getColors(): Result<List<Color>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getColors())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getColorsStream(ids: Set<Int>): Flow<Result<List<Color>>> {
        return dao.getColorsStream(ids).map {
            Result.Success(it)
        }
    }

    override suspend fun getColors(ids: Set<Int>): Result<List<Color>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getColors(ids))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveColor(color: Color) {
        dao.insertColor(color)
    }

    override suspend fun deleteAllColors() {
        dao.deleteColors()
    }

}