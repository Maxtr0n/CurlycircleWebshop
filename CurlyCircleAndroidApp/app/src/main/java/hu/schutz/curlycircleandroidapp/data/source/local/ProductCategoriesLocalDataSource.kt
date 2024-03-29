package hu.schutz.curlycircleandroidapp.data.source.local

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.Result.Success
import hu.schutz.curlycircleandroidapp.data.Result.Error
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import hu.schutz.curlycircleandroidapp.data.source.local.dao.ProductCategoriesDao
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import kotlinx.coroutines.withContext

class ProductCategoriesLocalDataSource internal constructor(
    private val dao: ProductCategoriesDao,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
) : ProductCategoriesDataSource {

    override fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>> {
        return dao.getProductCategoriesStream().map {
            Success(it)
        }
    }

    override suspend fun getProductCategories(): Result<List<ProductCategory>> = withContext(ioDispatcher) {
        return@withContext try {
            Success(dao.getProductCategories())
        } catch (e: Exception) {
            Error(e)
        }
    }

    override suspend fun saveProductCategory(productCategory: ProductCategory) {
        dao.insertProductCategory(productCategory)
    }

    override suspend fun deleteAllProductCategories() {
        dao.deleteProductCategories()
    }


}