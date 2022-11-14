package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.Color
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ColorsDataSource {

    fun getColorsStream(): Flow<Result<List<Color>>>

    suspend fun getColors(): Result<List<Color>>

    fun getColorsStream(ids: Set<Int>): Flow<Result<List<Color>>>

    suspend fun getColors(ids: Set<Int>): Result<List<Color>>

    suspend fun saveColor(color: Color)

    suspend fun deleteAllColors()
}