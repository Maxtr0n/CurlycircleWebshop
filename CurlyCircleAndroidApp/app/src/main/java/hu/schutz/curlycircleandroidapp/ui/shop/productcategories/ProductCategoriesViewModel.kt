package hu.schutz.curlycircleandroidapp.ui.shop.productcategories

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.ProductCategory
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.ProductCategoriesRepository
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

data class ShopUiState(
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
        .map { result ->
        if (result is Result.Success) {
            result.data
        } else {
            showSnackbarMessage(R.string.product_category_loading_error)
            emptyList()
        }
    }
        .map { productCategories ->
        Async.Success(productCategories)
    }
        .onStart<Async<List<ProductCategory>>> { emit(Async.Loading) }

    init {
        refreshProductCategories()
    }

    val uiState: StateFlow<ShopUiState> = combine(
        _isLoading, _userMessage, _productCategoriesAsync
    ) { isLoading, userMessage, productCategoriesAsync ->
        when (productCategoriesAsync) {
            Async.Loading -> {
                ShopUiState(isLoading = true)
            }
            is Async.Success -> {
                ShopUiState(
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
            initialValue = ShopUiState(isLoading = true)
        )


    fun refreshProductCategories() {
        _isLoading.value = true
        viewModelScope.launch {
            productCategoriesRepository.refreshProductCategories()
            _isLoading.value = false
        }
    }

    fun snackbarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackbarMessage(message: Int) {
        _userMessage.value = message
    }
}