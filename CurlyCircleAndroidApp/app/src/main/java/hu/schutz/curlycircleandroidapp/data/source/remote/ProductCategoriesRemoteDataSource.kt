package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.ProductCategoriesViewModel
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.source.ProductCategoriesDataSource
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import kotlinx.coroutines.withContext
import retrofit2.http.GET

class ProductCategoriesRemoteDataSource (
        private val api: CurlyCircleApi,
        private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
        ) : ProductCategoriesDataSource {

        private val observableProductCategories = MutableStateFlow(runBlocking { getProductCategories() })

        override fun getProductCategoriesStream(): Flow<Result<List<ProductCategory>>> {
                return observableProductCategories
        }

        override suspend fun getProductCategories(): Result<List<ProductCategory>> = withContext(ioDispatcher) {
                return@withContext try {
                        Result.Success(api.getProductCategories().productCategories)
                } catch (e: Exception) {
                        Result.Error(e)
                }
        }

        override suspend fun refreshProductCategories() {
                observableProductCategories.value = getProductCategories()
        }

        override suspend fun saveProductCategory(productCategory: ProductCategory) {
                // NO-OP
        }

        override suspend fun deleteAllProductCategories() {
                // NO-OP
        }

}