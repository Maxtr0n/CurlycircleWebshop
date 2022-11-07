package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ProductCategoriesRepository {
    fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>>
}