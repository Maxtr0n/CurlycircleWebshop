package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.Pattern
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.PatternsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext

class PatternsRemoteDataSource(
    private val api: CurlyCircleApi,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : PatternsDataSource {
    override fun getPatternsStream(): Flow<Result<List<Pattern>>> {
        return MutableStateFlow(
            runBlocking {
                getPatterns()
            }
        )
    }

    override suspend fun getPatterns(): Result<List<Pattern>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getPatterns().patterns)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getPatternStream(id: Int): Flow<Result<Pattern>> {
        return MutableStateFlow(
            runBlocking {
                getPattern(id)
            }
        )
    }

    override suspend fun getPattern(id: Int): Result<Pattern> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(api.getPattern(id))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun savePattern(pattern: Pattern) {
        // NO-OP
    }

    override suspend fun deleteAllPatterns() {
        // NO-OP
    }

}