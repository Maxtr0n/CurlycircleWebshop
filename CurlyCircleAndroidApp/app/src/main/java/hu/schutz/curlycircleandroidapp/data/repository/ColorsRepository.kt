package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Color
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ColorsRepository {

    fun getColorsStream(): Flow<Result<List<Color>>>

    suspend fun getColors(forceUpdate: Boolean = false): Result<List<Color>>

    fun getColorsStream(ids: Set<Int>):  Flow<Result<List<Color>>>

    suspend fun getColors(ids: Set<Int>, forceUpdate: Boolean = false): Result<List<Color>>
}