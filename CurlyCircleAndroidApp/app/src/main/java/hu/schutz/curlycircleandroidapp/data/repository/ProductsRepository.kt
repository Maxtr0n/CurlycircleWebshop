package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import kotlinx.coroutines.flow.Flow

interface ProductsRepository {

    fun getProductsStream(productQueryParameters: ProductQueryParameters): Flow<Result<List<Product>>>

    suspend fun getProducts(productQueryParameters: ProductQueryParameters, forceUpdate: Boolean = false): Result<List<Product>>

    fun getProductStream(productId: Int): Flow<Result<Product>>

    suspend fun getProduct(productId: Int, forceUpdate: Boolean = false): Result<Product>

    suspend fun saveProduct(product: Product): Result<Unit>
}