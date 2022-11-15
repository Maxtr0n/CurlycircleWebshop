package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Color
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ColorsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultColorsRepository(
    private val colorsRemoteDataSource: ColorsDataSource,
    private val colorsLocalDataSource: ColorsDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ColorsRepository {

    override fun getColorsStream(): Flow<Result<List<Color>>> {
        return colorsLocalDataSource.getColorsStream()
    }

    override suspend fun getColors(forceUpdate: Boolean): Result<List<Color>> {
        if (forceUpdate) {
            try {
                updateColorsFromRemoteDataSource()
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return colorsLocalDataSource.getColors()
    }

    override fun getColorsStream(ids: Set<Int>): Flow<Result<List<Color>>> {
        return colorsLocalDataSource.getColorsStream(ids)
    }

    override suspend fun getColors(ids: Set<Int>, forceUpdate: Boolean): Result<List<Color>> {
        if (forceUpdate) {
            try {
                updateColorFromRemoteDataSource(ids)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return colorsLocalDataSource.getColors(ids)
    }

    private suspend fun updateColorsFromRemoteDataSource() {
        val remoteColors = colorsRemoteDataSource.getColors()

        if (remoteColors is Result.Success) {
            colorsLocalDataSource.deleteAllColors()
            remoteColors.data.forEach { color ->
                colorsLocalDataSource.saveColor(color)
            }
        } else if (remoteColors is Result.Error) {
            throw remoteColors.exception
        }
    }

    private suspend fun updateColorFromRemoteDataSource(ids: Set<Int>) {
        val remoteColors = colorsRemoteDataSource.getColors(ids)

        if (remoteColors is Result.Success) {
            colorsLocalDataSource.deleteAllColors()
            remoteColors.data.forEach { color ->
                colorsLocalDataSource.saveColor(color)
            }
        } else if (remoteColors is Result.Error) {
            throw remoteColors.exception
        }
    }
}