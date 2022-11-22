package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import kotlinx.coroutines.flow.Flow

interface CartRepository {

    fun getCartItemsStream(): Flow<Result<List<CartItemAndProduct>>>

    suspend fun getCartItems(): Result<List<CartItemAndProduct>>

    suspend fun updateCartItem(cartItemId: Int, quantity: Int): Result<Unit>

    suspend fun addItemToCart(product: Product, quantity: Int): Result<Unit>

    suspend fun clearCart(): Result<Unit>

    suspend fun removeCartItem(cartItemId: Int): Result<Unit>

    suspend fun refreshCurrentCart(): Result<Unit>

    suspend fun handleUserChanged(user: User?): Result<Unit>

    fun getCurrentCartId(): Int
}