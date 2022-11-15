package hu.schutz.curlycircleandroidapp.ui.shop.productcategories

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.ProductCategoriesRepository
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

data class ProductCategoriesUiState(
    val productCategories: List<ProductCategory> = emptyList(),
    val isLoading: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class ProductCategoriesViewModel @Inject constructor(
    private val productCategoriesRepository: ProductCategoriesRepository
) : ViewModel() {

    private val _userMessage: MutableStateFlow<Int?> = MutableStateFlow(null)
    private  val _isLoading = MutableStateFlow(false)
    private val _productCategoriesAsync = productCategoriesRepository.getProductCategoriesStream()
        .map { handleResult(it) }
        .onStart { emit(Async.Loading) }

    init {
        getProductCategories()
    }

    val uiState: StateFlow<ProductCategoriesUiState> = combine(
        _isLoading, _userMessage, _productCategoriesAsync
    ) { isLoading, userMessage, productCategoriesAsync ->
        when (productCategoriesAsync) {
            Async.Loading -> {
                ProductCategoriesUiState(isLoading = true)
            }
            is Async.Success -> {
                ProductCategoriesUiState(
                    productCategories = productCategoriesAsync.data,
                    isLoading = isLoading,
                    userMessage = userMessage
                )
            }
        }

    }
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = ProductCategoriesUiState(isLoading = true)
        )


    private fun getProductCategories() {
        _isLoading.value = true
        viewModelScope.launch {
            productCategoriesRepository.getProductCategories(true)
            _isLoading.value = false
        }
    }

    fun snackBarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackBarMessage(message: Int) {
        _userMessage.value = message
    }

    private fun handleResult(productCategoriesResult: Result<List<ProductCategory>>): Async<List<ProductCategory>> =
        if (productCategoriesResult is Result.Success) {
            Async.Success(productCategoriesResult.data)
        } else {
            showSnackBarMessage(R.string.product_category_loading_error)
            Async.Success(emptyList())
        }
}