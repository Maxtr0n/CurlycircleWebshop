package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ProductCategoriesRepository {

    fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>>

    suspend fun getProductCategories(forceUpdate: Boolean = false): Result<List<ProductCategory>>

    suspend fun refreshProductCategories()
}