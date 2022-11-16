package hu.schutz.curlycircleandroidapp.data.source

import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ProductsDataSource {

    fun getProductsStream(productQueryParameters: ProductQueryParameters): Flow<Result<List<Product>>>

    suspend fun getProducts(productQueryParameters: ProductQueryParameters): Result<List<Product>>

    fun getProductStream(productId: Int): Flow<Result<Product>>

    suspend fun getProduct(productId: Int): Result<Product>

    suspend fun saveProduct(product: Product)

    suspend fun deleteProducts(productCategoryId: Int)
}