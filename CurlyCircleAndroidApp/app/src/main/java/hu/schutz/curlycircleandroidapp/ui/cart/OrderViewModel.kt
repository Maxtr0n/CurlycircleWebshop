package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.*
import hu.schutz.curlycircleandroidapp.data.repository.CartRepository
import hu.schutz.curlycircleandroidapp.data.repository.OrdersRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

data class OrderUiState(
    val email: String = "",
    val firstName: String = "",
    val lastName: String = "",
    val city: String = "",
    val zipCode: String = "",
    val line1: String = "",
    val line2: String = "",
    val phoneNumber: String = "",
    val note: String = "",
    val shippingMethod: ShippingMethod? = null,
    val paymentMethod: PaymentMethod? = null,
    val isLoading: Boolean = false,
    val orderSuccessful: Boolean = false,
    val userMessage: Int? = null
)

@HiltViewModel
class OrderViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val cartRepository: CartRepository,
    private val ordersRepository: OrdersRepository
) : ViewModel() {

    private var user: User? = null
    private var cartId: Int = 0

    private val _uiState = MutableStateFlow(OrderUiState())
    val uiState: StateFlow<OrderUiState> = _uiState.asStateFlow()

    init {
        _uiState.update { it.copy(isLoading = true) }
        viewModelScope.launch {
            cartId = cartRepository.getCurrentCartId()
            user = when (val result = userRepository.getUser()) {
                is Result.Success -> {
                    if (result.data != null) {
                        _uiState.update {
                            it.copy(
                                email = result.data.email, firstName = result.data.firstName,
                                lastName = result.data.lastName, city = result.data.city,
                                zipCode = result.data.zipCode, line1 = result.data.line1,
                                line2 = result.data.line2 ?: "", phoneNumber = result.data.phoneNumber
                            )
                        }
                    }
                    result.data
                }
                is Result.Error -> {
                    null
                }
            }
            _uiState.update { it.copy(isLoading = false) }
        }
    }

    fun updateEmail(newEmail: String) {
        _uiState.update {
            it.copy(email = newEmail)
        }
    }

    fun updateFirstName(newFirstName: String) {
        _uiState.update {
            it.copy(firstName = newFirstName)
        }
    }

    fun updateLastName(newLastName: String) {
        _uiState.update {
            it.copy(lastName = newLastName)
        }
    }

    fun updateCity(newCity: String) {
        _uiState.update {
            it.copy(city = newCity)
        }
    }

    fun updateZipCode(newZipCode: String) {
        _uiState.update {
            it.copy(zipCode = newZipCode)
        }
    }

    fun updateLine1(newLine1: String) {
        _uiState.update {
            it.copy(line1 = newLine1)
        }
    }

    fun updateLine2(newLine2: String) {
        _uiState.update {
            it.copy(line2 = newLine2)
        }
    }

    fun updatePhoneNumber(newPhoneNumber: String) {
        _uiState.update {
            it.copy(phoneNumber = newPhoneNumber)
        }
    }

    fun updateNote(newNote: String) {
        _uiState.update {
            it.copy(note = newNote)
        }
    }

    fun updateShippingMethod(newShippingMethod: ShippingMethod) {
        _uiState.update {
            it.copy(shippingMethod = newShippingMethod)
        }
    }

    fun updatePaymentMethod(newPaymentMethod: PaymentMethod) {
        _uiState.update {
            it.copy(paymentMethod = newPaymentMethod)
        }
    }

    fun placeOrder() {
        if (uiState.value.email.isEmpty() || uiState.value.firstName.isEmpty() ||
            uiState.value.lastName.isEmpty() ||
            uiState.value.city.isEmpty() || uiState.value.zipCode.isEmpty() ||
            uiState.value.line1.isEmpty() || uiState.value.phoneNumber.isEmpty() ||
                uiState.value.shippingMethod == null || uiState.value.paymentMethod == null) {
            _uiState.update {
                it.copy(userMessage = R.string.form_not_valid)
            }
            return
        }

       _uiState.update {
           it.copy(isLoading = true)
       }

        viewModelScope.launch {
            val orderUpsertDto = OrderUpsertDto(
                email = uiState.value.email,
                firstName = uiState.value.firstName,
                lastName = uiState.value.lastName,
                city = uiState.value.city,
                zipCode = uiState.value.zipCode,
                line1 = uiState.value.line1,
                line2 = uiState.value.line2,
                phoneNumber = uiState.value.phoneNumber,
                note = uiState.value.note,
                shippingMethod = uiState.value.shippingMethod!!,
                paymentMethod = uiState.value.paymentMethod!!,
                cartId = cartId,
                applicationUserId = user?.id
            )

            when (ordersRepository.placeOrder(orderUpsertDto)) {
                is Result.Success -> {
                    //navigáljunk át a cart screenre
                    _uiState.update {
                        it.copy(orderSuccessful = true, isLoading = false)
                    }
                }
                is Result.Error -> {
                    _uiState.update {
                        it.copy(isLoading = false, userMessage = R.string.order_unsuccessful)
                    }
                }
            }
        }

    }

    fun snackBarMessageShown() {
        _uiState.update {
            it.copy(userMessage = null)
        }
    }

    private fun showSnackBar(message: Int) {
        _uiState.update {
            it.copy(userMessage = message)
        }
    }
}