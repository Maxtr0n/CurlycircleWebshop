package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.ProductCategoriesRepository
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.ProductCategoriesLocalDataSource
import hu.schutz.curlycircleandroidapp.data.source.remote.ProductCategoriesRemoteDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow

class DefaultProductCategoriesRepository(
    private val productCategoriesRemoteDataSource: ProductCategoriesDataSource,
    private val productCategoriesLocalDataSource: ProductCategoriesDataSource,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductCategoriesRepository {

    override fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>> {
        return productCategoriesLocalDataSource.getProductCategoriesStream()
    }

    override suspend fun getProductCategories(forceUpdate: Boolean): Result<List<ProductCategory>> {
        if (forceUpdate) {
            try {
                updateProductCategoriesFromRemoteDataSource()
            } catch (ex: Exception) {
                return Result.Error(ex)
            }
        }
        return productCategoriesLocalDataSource.getProductCategories()
    }

    private suspend fun updateProductCategoriesFromRemoteDataSource() {
        val remoteProductCategories = productCategoriesRemoteDataSource.getProductCategories()

        if (remoteProductCategories is Result.Success) {
            // Itt lehet szükség lenne egy proper sync-re ahelyett hogy kitörlök mindent
            // lehet elég ha instertelek mindent mivel REPLACE az instert strategy? Test it!
            productCategoriesLocalDataSource.deleteAllProductCategories()
            remoteProductCategories.data.forEach{ productCategory ->
                productCategoriesLocalDataSource.saveProductCategory(productCategory)
            }
        } else if (remoteProductCategories is Result.Error) {
            throw remoteProductCategories.exception
        }
    }

}