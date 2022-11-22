package hu.schutz.curlycircleandroidapp.data.source.remote

import androidx.room.Delete
import hu.schutz.curlycircleandroidapp.data.*
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import retrofit2.http.Query


interface CurlyCircleApi {

    companion object {
        const val BASE_URL = "https://curlycircleapi.azurewebsites.net/api/"
    }

    @GET("productcategory")
    suspend fun getProductCategories(): ProductCategoriesViewModel

    @GET("product")
    suspend fun getProducts(
        @Query("productCategoryId") productCategoryId: Int,
        @Query("minPrice") minPrice: Int? = null,
        @Query("maxPrice") maxPrice: Int? = null,
        @Query("colorIds") colorIds: List<Int> = emptyList(),
        @Query("patternIds") patternIds: List<Int> = emptyList(),
        @Query("materialIds") materialIds: List<Int> = emptyList(),

    ): PagedProductsViewModel

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

    @POST("auth/login")
    suspend fun login(@Body loginDto: LoginDto): UserViewModel

    @POST("auth/register")
    suspend fun register(@Body registerDto: RegisterDto): EntityCreatedViewModel

    @POST("auth/refresh")
    suspend fun refreshToken(@Body refreshDto: RefreshDto): TokenViewModel

    @PUT("auth/update")
    suspend fun updateUser(@Body userUpdateDto: UserUpdateDto): UserDataViewModel

    @PUT("auth/change-password")
    suspend fun changePassword(@Body changePasswordDto: ChangePasswordDto)

    @GET("user/{userId}/orders")
    suspend fun getUserOrders(@Path("userId") userId: Int): OrdersViewModel

    @GET("user/{userId}/user-data")
    suspend fun getUserData(@Path("userId") userId: Int): UserDataViewModel

    @POST("order")
    suspend fun placeOrder(@Body orderUpsertDto: OrderUpsertDto): EntityCreatedViewModel

    @DELETE("cart/{cartId}/clear")
    suspend fun clearCart(@Path("cartId") cartId: Int): Response<Unit>

    @DELETE("cart/{cartId}/cartItems/{cartItemId}")
    suspend fun removeCartItem(
        @Path("cartId") cartId: Int,
        @Path("cartItemId") cartItemId: Int): Response<Unit>

    @GET("cart/{cartId}")
    suspend fun getCartById(@Path("cartId") cartId: Int): CartViewModel

    @GET("cart")
    suspend fun createCart(): EntityCreatedViewModel

    @POST("cart/{cartId}/cartItems")
    suspend fun addCartItem(
        @Path("cartId") cartId: Int,
        @Body cartItemUpsertDto: CartItemUpsertDto): EntityCreatedViewModel

    @PUT("cart/{cartId}/cartItems/{cartItemId}")
    suspend fun updateCartItem(
        @Path("cartId") cartId: Int,
        @Path("cartItemId") cartItemId: Int,
        @Body quantity: Int)
}