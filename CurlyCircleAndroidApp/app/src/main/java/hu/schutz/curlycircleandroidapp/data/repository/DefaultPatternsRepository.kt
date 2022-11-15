package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Pattern
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.PatternsDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultPatternsRepository(
    private val patternsRemoteDataSource: PatternsDataSource,
    private val patternsLocalDataSource: PatternsDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : PatternsRepository {

    override fun getPatternsStream(): Flow<Result<List<Pattern>>> {
        return patternsLocalDataSource.getPatternsStream()
    }

    override suspend fun getPatterns(forceUpdate: Boolean): Result<List<Pattern>> {
        if (forceUpdate) {
            try {
                updatePatternsFromRemoteDataSource()
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return patternsLocalDataSource.getPatterns()
    }

    override fun getPatternStream(id: Int): Flow<Result<Pattern>> {
        return patternsLocalDataSource.getPatternStream(id)
    }

    override suspend fun getPattern(id: Int, forceUpdate: Boolean): Result<Pattern> {
        if (forceUpdate) {
            try {
                updatePatternFromRemoteDataSource(id)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return patternsLocalDataSource.getPattern(id)
    }

    private suspend fun updatePatternsFromRemoteDataSource() {
        val remotePatterns = patternsRemoteDataSource.getPatterns()

        if (remotePatterns is Result.Success) {
            patternsLocalDataSource.deleteAllPatterns()
            remotePatterns.data.forEach { pattern ->
                patternsLocalDataSource.savePattern(pattern)
            }
        } else if (remotePatterns is Result.Error) {
            throw remotePatterns.exception
        }
    }

    private suspend fun updatePatternFromRemoteDataSource(id: Int) {
        val remotePattern = patternsRemoteDataSource.getPattern(id)

        if (remotePattern is Result.Success) {
            patternsLocalDataSource.savePattern(remotePattern.data)

        } else if (remotePattern is Result.Error) {
            throw remotePattern.exception
        }
    }
}