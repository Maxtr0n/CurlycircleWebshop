package hu.schutz.curlycircleandroidapp.data.source.remote

import hu.schutz.curlycircleandroidapp.data.*
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query


interface CurlyCircleApi {

    companion object {
        const val BASE_URL = "https://curlycircleapi.azurewebsites.net/api/"
    }

    @GET("productcategory")
    suspend fun getProductCategories(): ProductCategoriesViewModel

    @GET("product")
    suspend fun getProducts(@Query("productQueryParameters") productQueryParameters: ProductQueryParameters): PagedProductsViewModel

    @GET("product/{productId}")
    suspend fun getProductById(@Path("productId") id: Int): ProductViewModel

    @GET("color")
    suspend fun getColors(): ColorsViewModel

    @GET("color/{id}")
    suspend fun getColor(@Path("id") id: Int): Color

    @GET("pattern")
    suspend fun getPatterns(): PatternsViewModel

    @GET("pattern/{id}")
    suspend fun getPattern(@Path("id") id: Int): Pattern

    @GET("material")
    suspend fun getMaterials(): MaterialsViewModel

    @GET("material/{id}")
    suspend fun getMaterial(@Path("id") id: Int): Material
}