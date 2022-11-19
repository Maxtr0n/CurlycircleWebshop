package hu.schutz.curlycircleandroidapp.ui.account

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_ID_ARG
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.data.Result
import hu.schutz.curlycircleandroidapp.data.repository.OrdersRepository
import hu.schutz.curlycircleandroidapp.util.Async
import hu.schutz.curlycircleandroidapp.util.WhileUiSubscribed
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import javax.inject.Inject

data class OrdersUiState(
    val orders: List<Order> = emptyList(),
    val isLoading: Boolean = false,
    val userMessage: Int? = null
)
@HiltViewModel
class OrdersViewModel @Inject constructor(
    private val ordersRepository: OrdersRepository,
    savedStateHandle: SavedStateHandle
) : ViewModel() {

    private val userId: Int = savedStateHandle[USER_ID_ARG]!!

    private val _userMessage: MutableStateFlow<Int?> = MutableStateFlow(null)
    private  val _isLoading = MutableStateFlow(false)
    private val _ordersAsync = ordersRepository.getOrdersStream(userId)
        .map { handleOrdersResult(it) }
        .onStart { emit(Async.Loading) }


    init {
        getOrders(userId)
    }

    val uiState: StateFlow<OrdersUiState> = combine(
        _userMessage, _isLoading, _ordersAsync
    ) { userMessage, isLoading, ordersAsync ->
        when(ordersAsync) {
            Async.Loading -> {
                OrdersUiState(isLoading = true)
            }
            is Async.Success -> {
                OrdersUiState(
                    orders = ordersAsync.data,
                    isLoading = isLoading,
                    userMessage = userMessage
                )
            }
        }
    }
        .stateIn(
            scope = viewModelScope,
            started = WhileUiSubscribed,
            initialValue = OrdersUiState(isLoading = true)
        )

    fun snackBarMessageShown() {
        _userMessage.value = null
    }

    private fun showSnackBarMessage(message: Int) {
        _userMessage.value = message
    }

    private fun getOrders(id: Int) {
        _isLoading.value = true
        viewModelScope.launch {
            ordersRepository.getOrders(id, true)
            _isLoading.value = false
        }
    }

    private fun handleOrdersResult(ordersResult: Result<List<Order>>): Async<List<Order>> =
        if (ordersResult is Result.Success) {
            Async.Success(ordersResult.data)
        } else {
            showSnackBarMessage(R.string.products_loading_error)
            Async.Success(emptyList())
        }
}