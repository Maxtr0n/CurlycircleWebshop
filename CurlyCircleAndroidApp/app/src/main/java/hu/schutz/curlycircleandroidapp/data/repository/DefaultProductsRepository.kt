package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ProductsDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.ProductsLocalDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultProductsRepository (
    private val productsRemoteDataSource: ProductsDataSource,
    private val productsLocalDataSource: ProductsDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductsRepository {

    override fun getProductsStream(productQueryParameters: ProductQueryParameters): Flow<Result<List<Product>>> {
        return productsLocalDataSource.getProductsStream(productQueryParameters)
    }

    override suspend fun getProducts(productQueryParameters: ProductQueryParameters, forceUpdate: Boolean): Result<List<Product>> {
        if (forceUpdate) {
            try {
                updateProductsFromRemoteDataSource(productQueryParameters)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return productsLocalDataSource.getProducts(productQueryParameters)
    }

    override fun getProductStream(productId: Int): Flow<Result<Product>> {
        return productsLocalDataSource.getProductStream(productId)
    }

    override suspend fun getProduct(productId: Int, forceUpdate: Boolean): Result<Product> {
        if (forceUpdate) {
            try {
                updateProductFromRemoteDataSource(productId)
            } catch (e: Exception) {
                return Result.Error(e)
            }
        }
        return productsLocalDataSource.getProduct(productId)
    }

    private suspend fun updateProductsFromRemoteDataSource(productQueryParameters: ProductQueryParameters) {
        val remoteProducts = productsRemoteDataSource.getProducts(productQueryParameters)

        if (remoteProducts is Result.Success) {
            productsLocalDataSource.deleteAllProducts()
            remoteProducts.data.forEach{ product ->
                productsLocalDataSource.saveProduct(product)
            }
        } else if (remoteProducts is Result.Error) {
            throw remoteProducts.exception
        }
    }

    private suspend fun updateProductFromRemoteDataSource(productId: Int) {
        val remoteProduct = productsRemoteDataSource.getProduct(productId)

        if (remoteProduct is Result.Success) {
                productsLocalDataSource.saveProduct(remoteProduct.data)
        } else if (remoteProduct is Result.Error) {
            throw remoteProduct.exception
        }
    }
}