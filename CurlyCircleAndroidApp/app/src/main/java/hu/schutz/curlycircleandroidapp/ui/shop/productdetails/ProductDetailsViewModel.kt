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
    private val _userMessage: MutableStateFlow<Int?> = MutableStateFlow(null)
    private  val _isLoading = MutableStateFlow(false)
    private val _productAsync = productsRepository.getProductStream(productId)
        .map { handleProductResult(it) }
        .onStart { emit(Async.Loading) }
    private val _quantity = mutableStateOf(1)

    init {
        getProduct(productId)
    }

    val uiState: StateFlow<ProductDetailsUiState> = combine(
        _userMessage, _isLoading, _productAsync
    ) { userMessage, isLoading, productAsync ->
        when(productAsync) {
            Async.Loading -> {
                ProductDetailsUiState(isLoading = true)
            }
            is Async.Success -> {
                ProductDetailsUiState(
                    product = productAsync.data,
                    isLoading = isLoading,
                    userMessage = userMessage
                )
            }
        }
    }
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = ProductDetailsUiState(isLoading = true)
        )

    fun addProductToCart(product: Product) {
        _isLoading.value = true
        viewModelScope.launch {
            cartRepository.addItemToCart(product, 1)
            _isLoading.value = false
        }
    }

    fun snackBarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackBarMessage(message: Int) {
        _userMessage.value = message
    }

    private fun handleProductResult(productResult: Result<Product>): Async<Product?> =
        if (productResult is Result.Success) {
            Async.Success(productResult.data)
        } else {
            showSnackBarMessage(R.string.product_loading_error)
            Async.Success(null)
        }

    private fun getProduct(id: Int) {
        _isLoading.value = true
        viewModelScope.launch {
            productsRepository.getProduct(id, true)
            _isLoading.value = false
        }
    }
}