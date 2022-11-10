package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ProductCategoriesDataSource {

    fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>>

    suspend fun getProductCategories(): Result<List<ProductCategory>>

    suspend fun refreshProductCategories()

    suspend fun saveProductCategory(productCategory: ProductCategory)

    suspend fun deleteAllProductCategories()

}