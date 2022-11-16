package hu.schutz.curlycircleandroidapp.ui.shop.products

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.repository.*
import hu.schutz.curlycircleandroidapp.ui.shop.ShopDestinationArgs
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

data class ProductsUiState(
    val products: List<Product> = emptyList(),
    val isLoading: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class ProductsViewModel @Inject constructor(
    private val productsRepository: ProductsRepository,
    private val productCategoriesRepository: ProductCategoriesRepository,
    private val colorsRepository: ColorsRepository,
    private val materialsRepository: MaterialsRepository,
    private val patternsRepository: PatternsRepository,
    savedStateHandle: SavedStateHandle
) : ViewModel() {

    private val productCategoryId: Int = savedStateHandle[ShopDestinationArgs.PRODUCT_CATEGORY_ID_ARG]!!
    private val _userMessage: MutableStateFlow<Int?> = MutableStateFlow(null)
    private  val _isLoading = MutableStateFlow(false)

    private val _productsAsync = productsRepository.getProductsStream(ProductQueryParameters(productCategoryId = productCategoryId))
        .map { handleProductsResult(it) }
        .onStart { emit(Async.Loading) }

    init {
        getProducts(productCategoryId)
    }

    val uiState: StateFlow<ProductsUiState> = combine(
        _userMessage, _isLoading, _productsAsync
    ) { userMessage, isLoading, productsAsync ->
        when(productsAsync) {
            Async.Loading -> {
                ProductsUiState(isLoading = true)
            }
            is Async.Success -> {
                ProductsUiState(
                    products = productsAsync.data,
                    isLoading = isLoading,
                    userMessage = userMessage
                )
            }
        }
    }
        .stateIn(
        scope = viewModelScope,
        started = WhileUiSubscribed,
        initialValue = ProductsUiState(isLoading = true)
    )

    fun snackBarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackBarMessage(message: Int) {
        _userMessage.value = message
    }

    private fun handleProductsResult(productsResult: Result<List<Product>>): Async<List<Product>> =
        if (productsResult is Result.Success) {
            Async.Success(productsResult.data)
        } else {
            showSnackBarMessage(R.string.products_loading_error)
            Async.Success(emptyList())
        }

    private fun handleColorsResult(colorsResult: Result<List<Color>>): Async<List<Color>> =
        if (colorsResult is Result.Success) {
            Async.Success(colorsResult.data)
        } else {
            Async.Success(emptyList())
        }

    private fun handleMaterialsResult(materialsResult: Result<List<Material>>): Async<List<Material>> =
        if (materialsResult is Result.Success) {
            Async.Success(materialsResult.data)
        } else {
            Async.Success(emptyList())
        }

    private fun handlePatternsResult(patternsResult: Result<List<Pattern>>): Async<List<Pattern>> =
        if (patternsResult is Result.Success) {
            Async.Success(patternsResult.data)
        } else {
            Async.Success(emptyList())
        }

    private fun getProducts(id: Int) {
        _isLoading.value = true
        viewModelScope.launch {
            productsRepository.getProducts(ProductQueryParameters(productCategoryId = id), true)
            _isLoading.value = false
        }
    }
}