package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.Pattern
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.PatternsDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.PatternsDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class PatternsLocalDataSource internal constructor(
    private val dao: PatternsDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : PatternsDataSource {

    override fun getPatternsStream(): Flow<Result<List<Pattern>>> {
        return dao.getPatternsStream().map {
           Result.Success(it)
        }
    }

    override suspend fun getPatterns(): Result<List<Pattern>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getPatterns())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getPatternStream(id: Int): Flow<Result<Pattern>> {
        return dao.getPatternStream(id).map {
            Result.Success(it)
        }
    }

    override suspend fun getPattern(id: Int): Result<Pattern> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getPattern(id))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun savePattern(pattern: Pattern) {
        dao.insertPattern(pattern)
    }

    override suspend fun deleteAllPatterns() {
        dao.deletePatterns()
    }

}