package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ProductsDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.ProductsDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class ProductsLocalDataSource internal constructor(
    private val dao: ProductsDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductsDataSource {

    override fun getProductsStream(productQueryParameters: ProductQueryParameters):
            Flow<Result<List<Product>>> {
        return dao.getProductsStream(productQueryParameters.productCategoryId).map {
            Result.Success(it)
        }
    }

    override suspend fun getProducts(
        productQueryParameters: ProductQueryParameters
    ): Result<List<Product>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getProducts(productQueryParameters.productCategoryId))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override fun getProductStream(productId: Int): Flow<Result<Product>> {
        return dao.getProductStream(productId).map {
            Result.Success(it)
        }
    }

    override suspend fun getProduct(productId: Int): Result<Product> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getProduct(productId))
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun saveProduct(product: Product) {
        dao.insertProduct(product)
    }

    override suspend fun deleteProducts(productCategoryId: Int) {
        dao.deleteProducts(productCategoryId)
    }

}