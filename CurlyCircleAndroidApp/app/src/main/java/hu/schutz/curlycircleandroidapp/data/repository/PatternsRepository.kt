package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Pattern
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface PatternsRepository {

    fun getPatternsStream(): Flow<Result<List<Pattern>>>

    suspend fun getPatterns(forceUpdate: Boolean = false): Result<List<Pattern>>

    fun getPatternStream(id: Int): Flow<Result<Pattern>>

    suspend fun getPattern(id: Int, forceUpdate: Boolean = false): Result<Pattern>
}