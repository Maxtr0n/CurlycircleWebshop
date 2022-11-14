package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.Pattern
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface PatternsDataSource {

    fun getPatternsStream(): Flow<Result<List<Pattern>>>

    suspend fun getPatterns(): Result<List<Pattern>>

    fun getPatternStream(id: Int): Flow<Result<Pattern>>

    suspend fun getPattern(id: Int): Result<Pattern>

    suspend fun savePattern(pattern: Pattern)

    suspend fun deleteAllPatterns()
}