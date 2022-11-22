package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.CartItem
import hu.schutz.curlycircleandroidapp.data.CartItemAndProduct
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.CartRepository
import hu.schutz.curlycircleandroidapp.data.repository.ProductsRepository
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

data class CartUiState(
    val cartItems: List<CartItemAndProduct> = emptyList(),
    val isLoading: Boolean = false,
    val readyToCheckout: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class CartViewModel @Inject constructor(
    private val cartRepository: CartRepository,
    private val productsRepository: ProductsRepository
) : ViewModel() {

    private val _uiState = MutableStateFlow(CartUiState(isLoading = true))

    val uiState = _uiState
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = _uiState.value
        )

    init {
        getCartItems()

        viewModelScope.launch {
            cartRepository.getCartItemsStream().collect { result ->
                when(result) {
                    is Result.Success -> {
                        _uiState.update { it.copy(cartItems = result.data) }
                    }
                    is Result.Error -> {
                        _uiState.update { it.copy(cartItems = emptyList()) }
                    }
                }
            }
        }
    }

    private fun getCartItems() {
        _uiState.update { it.copy(isLoading = true) }

        viewModelScope.launch{
            val result = cartRepository.refreshCurrentCart()
            _uiState.update {
                when (result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false)
                }
            }
        }
    }

    fun removeCartItem(cartItem: CartItem) {
        _uiState.update { it.copy(isLoading = true) }

        viewModelScope.launch{
            val result = cartRepository.removeCartItem(cartItem.id)

            _uiState.update {
                when (result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false, userMessage = R.string.error_try_again)
                }
            }
        }
    }

    fun clearCart() {
        _uiState.update { it.copy(isLoading = true) }

        viewModelScope.launch{
            val result = cartRepository.clearCart()

            _uiState.update {
                when (result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false, userMessage = R.string.error_try_again)
                }
            }
        }
    }

    fun increaseQuantity(cartItem: CartItem) {
        if (cartItem.quantity >= 10) {
            _uiState.update { it.copy(userMessage = R.string.error_quantity_maximum_reached) }
            return
        }

        _uiState.update { it.copy(isLoading = true) }

        viewModelScope.launch{
            val result = cartRepository.updateCartItem(
                cartItemId = cartItem.id,
                quantity = cartItem.quantity + 1
            )

            _uiState.update {
                when (result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false, userMessage = R.string.error_try_again)
                }
            }
        }
    }

    fun decreaseQuantity(cartItem: CartItem) {
        _uiState.update { it.copy(isLoading = true) }

        viewModelScope.launch{
            val result:Result<Unit> = cartRepository.updateCartItem(
                    cartItemId = cartItem.id,
                    quantity = cartItem.quantity - 1
                )

            _uiState.update {
                when (result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false,
                        userMessage = R.string.error_try_again)
                }
            }
        }
    }

    fun checkout() {
        if (_uiState.value.cartItems.isEmpty()) {
            _uiState.update {
                it.copy(userMessage = R.string.error_cart_empty)
            }
            return
        }

        _uiState.update {
            it.copy(readyToCheckout = true)
        }
    }

    fun snackBarMessageShown() {
        _uiState.update {
            it.copy(userMessage = null)
        }
    }
}