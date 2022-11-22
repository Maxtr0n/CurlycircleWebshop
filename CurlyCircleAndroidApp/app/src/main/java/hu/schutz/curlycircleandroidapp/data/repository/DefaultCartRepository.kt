package hu.schutz.curlycircleandroidapp.data.repository

import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.source.local.AppSharedPreferences
import hu.schutz.curlycircleandroidapp.data.source.local.dao.CartItemsDao
import hu.schutz.curlycircleandroidapp.data.source.remote.CurlyCircleApi
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

class DefaultCartRepository(
    private val api: CurlyCircleApi,
    private val dao: CartItemsDao,
    private val sharedPreferences: AppSharedPreferences,
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO,
    private val externalScope: CoroutineScope,
    private val userRepository: UserRepository,
    private val productsRepository: ProductsRepository
) : CartRepository {

    init {
        externalScope.launch {
            userRepository.getUserStream().collect { result ->
                if (result is Result.Success) {
                    handleUserChanged(result.data)
                } else {
                    handleUserChanged(null)
                }
            }
        }
    }

    override fun getCartItemsStream(): Flow<Result<Map<CartItem, Product>>> {
        return dao.getCartItemsStream().map {
            Result.Success(it)
        }
    }

    override suspend fun getCartItems(): Result<Map<CartItem, Product>> = withContext(ioDispatcher) {
        return@withContext try {
            Result.Success(dao.getCartItems())
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    override suspend fun updateCartItem(cartItemId: Int, quantity: Int) =
        withContext(ioDispatcher) {
            return@withContext try {
                val cartId = sharedPreferences.getCardId()
                api.updateCartItem(cartId = cartId, cartItemId = cartItemId, quantity = quantity)
                updateCartItemsFromRemoteDataSource(cartId)
                Result.Success(Unit)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun addItemToCart(product: Product, quantity: Int): Result<Unit> =
        withContext(ioDispatcher) {
            return@withContext try {
                var cartId = sharedPreferences.getCardId()
                val cartItemUpsertDto = CartItemUpsertDto(
                    product.id,
                    product.price,
                    quantity
                )

                if (cartId != 0) {
                    api.addCartItem(cartId, cartItemUpsertDto)
                    updateCartItemsFromRemoteDataSource(cartId)
                } else {
                    cartId = api.createCart().id
                    sharedPreferences.setCartId(cartId)
                    api.addCartItem(cartId, cartItemUpsertDto)
                    updateCartItemsFromRemoteDataSource(cartId)
                }

                Result.Success(Unit)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun clearCart() =
        withContext(ioDispatcher) {
            return@withContext try {
                val cartId = sharedPreferences.getCardId()
                api.clearCart(cartId)
                updateCartItemsFromRemoteDataSource(cartId)
                Result.Success(Unit)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun removeCartItem(cartItemId: Int)  =
        withContext(ioDispatcher) {
            return@withContext try {
                val cartId = sharedPreferences.getCardId()
                api.removeCartItem(cartId, cartItemId)
                updateCartItemsFromRemoteDataSource(cartId)
                Result.Success(Unit)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override suspend fun refreshCurrentCart() =
        withContext(ioDispatcher) {
            return@withContext try {
                val cartId = sharedPreferences.getCardId()
                updateCartItemsFromRemoteDataSource(cartId)
                Result.Success(Unit)
            } catch (e: Exception) {
                Result.Error(e)
            }
        }

    override fun getCurrentCartId(): Int {
        return sharedPreferences.getCardId()
    }

    override suspend fun handleUserChanged(user: User?): Result<Unit> = withContext(ioDispatcher) {
        return@withContext try {
            if (user != null) {
                // user logged in
                sharedPreferences.setCartId(user.cartId)
                updateCartItemsFromRemoteDataSource(user.cartId)
            } else {
                // user logged out
                sharedPreferences.setCartId(0)
                dao.deleteCartItems()
            }
            Result.Success(Unit)
        } catch (e: Exception) {
            Result.Error(e)
        }
    }

    private suspend fun updateCartItemsFromRemoteDataSource(cartId: Int){
        val cartViewModel = api.getCartById(cartId)
        dao.deleteCartItems()
        cartViewModel.cartItems.forEach { item ->
            dao.insertCartItem(
                CartItem(
                    id = item.id, cartId = item.cartId, productId = item.productId,
                    price = item.price, quantity = item.quantity)
            )

            val productViewModel = item.product
            val product = Product(
                id = productViewModel.id,
                name = productViewModel.name,
                description = productViewModel.description,
                material = productViewModel.material?.name ?: "",
                pattern = productViewModel.pattern?.name ?: "",
                colors = productViewModel.colors.map { it.name },
                thumbnailImageUrl = productViewModel.thumbnailImageUrl,
                imageUrls = productViewModel.imageUrls,
                price = productViewModel.price,
                productCategoryId = productViewModel.productCategoryId,
                isAvailable = productViewModel.isAvailable
            )
            productsRepository.saveProduct(product)
        }
    }

}