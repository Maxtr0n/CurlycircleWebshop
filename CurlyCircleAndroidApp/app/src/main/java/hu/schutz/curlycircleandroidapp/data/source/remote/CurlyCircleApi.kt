package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.ProductCategoriesViewModel
import retrofit2.http.GET


interface CurlyCircleApi {

    companion object {
        const val BASE_URL = "https://curlycircleapi.azurewebsites.net/api/"
    }

    @GET("productcategory")
    suspend fun getProductCategories(): ProductCategoriesViewModel
}