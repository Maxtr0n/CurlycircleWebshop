package hu.schutz.curlycircleandroidapp.ui.shop.productdetails

import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.PRODUCT_ID_ARG
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductQueryParameters
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.CartRepository
import hu.schutz.curlycircleandroidapp.data.repository.ProductsRepository
import hu.schutz.curlycircleandroidapp.ui.shop.products.ProductsUiState
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

data class ProductDetailsUiState(
    val product: Product? = null,
    val isLoading: Boolean = false,
    val userMessage: Int? = null,
    val quantity: Int = 1
    )

@HiltViewModel
class ProductDetailsViewModel @Inject constructor(
    private val productsRepository: ProductsRepository,
    private val cartRepository: CartRepository,
    savedStateHandle: SavedStateHandle
) : ViewModel() {
    private val productId: Int = savedStateHandle[PRODUCT_ID_ARG]!!
    private val _uiState = MutableStateFlow(ProductDetailsUiState(isLoading = true))

    val uiState: StateFlow<ProductDetailsUiState> = _uiState
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = _uiState.value
        )

    init {
        getProduct(productId)

        viewModelScope.launch {
            productsRepository.getProductStream(productId).collect() { result ->
                when(result) {
                    is Result.Success -> {
                        _uiState.update { it.copy(product = result.data) }
                    }
                    is Result.Error -> {
                        _uiState.update { it.copy(product = null) }
                    }
                }
            }
        }
    }

    fun addProductToCart(product: Product, quantity: Int) {
        _uiState.update { it.copy(isLoading = true) }
        viewModelScope.launch {
            val result = cartRepository.addItemToCart(product, quantity)
            _uiState.update {
                when(result) {
                    is Result.Success -> it.copy(isLoading = false)
                    is Result.Error -> it.copy(isLoading = false,
                        userMessage = R.string.error_try_again)
                }
            }
        }
    }

    fun increaseQuantity() {
        if (_uiState.value.quantity >= 10) {
            _uiState.update {
                it.copy(userMessage = R.string.error_quantity_maximum_reached)
            }
        }

        _uiState.update {
            it.copy(quantity = _uiState.value.quantity + 1)
        }
    }

    fun decreaseQuantity() {
        if (_uiState.value.quantity <= 1) {
            _uiState.update {
                it.copy(userMessage = R.string.error_quantity_minimum_reached)
            }
        }

        _uiState.update {
            it.copy(quantity = _uiState.value.quantity - 1)
        }
    }

    fun snackBarMessageShown() {
        _uiState.update {
            it.copy(userMessage = null)
        }
    }

    private fun showSnackBarMessage(message: Int) {
        _uiState.update {
            it.copy(userMessage = message)
        }
    }

    private fun handleProductResult(productResult: Result<Product>): Async<Product?> =
        if (productResult is Result.Success) {
            Async.Success(productResult.data)
        } else {
            showSnackBarMessage(R.string.product_loading_error)
            Async.Success(null)
        }

    private fun getProduct(id: Int) {
        _uiState.update { it.copy(isLoading = true) }
        viewModelScope.launch {
            val result = productsRepository.getProduct(id, true)
            _uiState.update {
                when(result) {
                    is Result.Success -> it.copy(product = result.data, isLoading = false)
                    is Result.Error -> it.copy(product = null, isLoading = false)
                }
            }
        }
    }
}