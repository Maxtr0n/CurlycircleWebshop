package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.Color
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ColorsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext

class ColorsRemoteDataSource(
    private val api: CurlyCircleApi,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ColorsDataSource {
    override fun getColorsStream(): Flow<Result<List<Color>>> {
        return MutableStateFlow(
            runBlocking {
                getColors()
            }
        )
    }

    override suspend fun getColors(): Result<List<Color>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getColors().colors)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getColorsStream(ids: Set<Int>): Flow<Result<List<Color>>> {
        return MutableStateFlow(
            runBlocking {
                getColors(ids)
            }
        )
    }

    override suspend fun getColors(ids: Set<Int>): Result<List<Color>> = withContext(ioDispatcher) {
        return@withContext try {
            val allColors = api.getColors().colors
            val colors = allColors.filter { color ->
                color.id in ids
            }
            Result.Success(colors)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveColor(color: Color) {
        // NO-OP
    }

    override suspend fun deleteAllColors() {
        // NO-OP
    }

}