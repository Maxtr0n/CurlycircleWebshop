package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import hu.schutz.curlycircleandroidapp.data.PaymentMethod
import hu.schutz.curlycircleandroidapp.data.ShippingMethod
import hu.schutz.curlycircleandroidapp.data.repository.OrdersRepository
import hu.schutz.curlycircleandroidapp.data.repository.UserRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
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
    private val ordersRepository: OrdersRepository
) : ViewModel() {

    private val _uiState = MutableStateFlow(OrderUiState())
    val uiState: StateFlow<OrderUiState> = _uiState.asStateFlow()

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
        // TODO
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