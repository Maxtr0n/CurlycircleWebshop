package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.ProductCategory
import kotlinx.coroutines.CoroutineDispatcher
import retrofit2.http.GET

class ProductCategoriesRemoteDataSource (
        private val api: ProductCategoriesApi,
        private val ioDispatcher: CoroutineDispatcher
        ) {

}

interface ProductCategoriesApi {

        companion object {
                const val BASE_URL = "http://localhost:5000/api/productcategory"
        }

        @GET
        suspend fun getProductCategories(): List<ProductCategory>
}