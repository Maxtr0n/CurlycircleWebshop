package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.selection.selectable
import androidx.compose.foundation.selection.selectableGroup
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.semantics.Role
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.PaymentMethod
import hu.schutz.curlycircleandroidapp.data.ShippingMethod
import hu.schutz.curlycircleandroidapp.util.convertPaymentMethod
import hu.schutz.curlycircleandroidapp.util.convertShippingMethod

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun OrderScreen(
    viewModel: OrderViewModel = hiltViewModel(),
    onSuccessfulOrder: () -> Unit,
    scaffoldState: ScaffoldState
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    OrderContent(
        loading = uiState.isLoading,
        email = uiState.email,
        firstName = uiState.firstName,
        lastName = uiState.lastName,
        city = uiState.city,
        zipCode = uiState.zipCode,
        line1 = uiState.line1,
        line2 = uiState.line2,
        phoneNumber = uiState.phoneNumber,
        note = uiState.note,
        selectedPaymentMethod = uiState.paymentMethod,
        selectedShippingMethod = uiState.shippingMethod,
        onEmailChanged = viewModel::updateEmail,
        onFirstNameChanged = viewModel::updateFirstName,
        onLastNameChanged = viewModel::updateLastName,
        onCityChanged = viewModel::updateCity,
        onZipCodeChanged = viewModel::updateZipCode,
        onLine1Changed = viewModel::updateLine1,
        onLine2Changed = viewModel::updateLine2,
        onPhoneNumberChanged = viewModel::updatePhoneNumber,
        onNoteChanged = viewModel::updateNote,
        onShippingMethodSelected = viewModel::updateShippingMethod,
        onPaymentMethodSelected = viewModel::updatePaymentMethod,
        onOrderClick = viewModel::placeOrder
    )

    LaunchedEffect(uiState.orderSuccessful) {
        if (uiState.orderSuccessful) {
            onSuccessfulOrder()
        }
    }

    // Check for user messages to display on the screen
    uiState.userMessage?.let { userMessage ->
        val snackBarText = stringResource(userMessage)
        LaunchedEffect(scaffoldState, viewModel, userMessage, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackBarMessageShown()
        }
    }
}

@Composable
fun OrderContent(
    loading: Boolean,
    email: String,
    firstName: String,
    lastName: String,
    city: String,
    zipCode: String,
    line1: String,
    line2: String,
    phoneNumber: String,
    note: String,
    selectedPaymentMethod: PaymentMethod?,
    selectedShippingMethod: ShippingMethod?,
    onEmailChanged: (String) -> Unit,
    onFirstNameChanged: (String) -> Unit,
    onLastNameChanged: (String) -> Unit,
    onCityChanged: (String) -> Unit,
    onZipCodeChanged: (String) -> Unit,
    onLine1Changed: (String) -> Unit,
    onLine2Changed: (String) -> Unit,
    onPhoneNumberChanged: (String) -> Unit,
    onNoteChanged: (String) -> Unit,
    onShippingMethodSelected: (ShippingMethod) -> Unit,
    onPaymentMethodSelected: (PaymentMethod) -> Unit,
    onOrderClick: () -> Unit,
    modifier: Modifier = Modifier
) {

    if (loading) {
        Box(
            modifier = modifier.fillMaxSize(),
            contentAlignment = Alignment.Center
        ) {
            CircularProgressIndicator()
        }
    } else {
        Column(
            modifier = modifier
                .fillMaxSize()
                .padding(16.dp)
                .verticalScroll(rememberScrollState()),
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(
                text = stringResource(id = R.string.personal_data),
                style = MaterialTheme.typography.h5
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.email_address)) },
                value = email,
                onValueChange = onEmailChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.last_name)) },
                value = lastName,
                onValueChange = onLastNameChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.first_name)) },
                value = firstName,
                onValueChange = onFirstNameChanged
            )
            Spacer(modifier = Modifier.height(20.dp))

            TextField(
                label = { Text(text = stringResource(R.string.city)) },
                value = city,
                onValueChange = onCityChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.zipCode)) },
                value = zipCode,
                onValueChange = onZipCodeChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.line1)) },
                value = line1,
                onValueChange = onLine1Changed
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.line2)) },
                value = line2,
                onValueChange = onLine2Changed
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.phoneNumber)) },
                value = phoneNumber,
                onValueChange = onPhoneNumberChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.note)) },
                value = note,
                onValueChange = onNoteChanged
            )

            Spacer(modifier = Modifier.height(20.dp))
            Text(
                text = stringResource(id = R.string.payment_method),
                style = MaterialTheme.typography.h5
            )
            Column(modifier = Modifier.selectableGroup()) {
                PaymentMethod.values().forEach { paymentMethod ->
                    Row(
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(56.dp)
                            .selectable(
                                selected = (paymentMethod == selectedPaymentMethod),
                                onClick = { onPaymentMethodSelected(paymentMethod) },
                                role = Role.RadioButton
                            )
                            .padding(horizontal = 16.dp),
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        RadioButton(
                            selected = (paymentMethod == selectedPaymentMethod),
                            onClick = null // null recommended for accessibility with screenreaders
                        )
                        Text(
                            text = convertPaymentMethod(paymentMethod),
                            style = MaterialTheme.typography.body1,
                            modifier = Modifier.padding(start = 16.dp)
                        )
                    }
                }
            }

            Spacer(modifier = Modifier.height(20.dp))
            Text(
                text = stringResource(id = R.string.shipping_method),
                style = MaterialTheme.typography.h5,
            )

            Column(modifier = Modifier.selectableGroup()) {
                ShippingMethod.values().forEach { shippingMethod ->
                    Row(
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(56.dp)
                            .selectable(
                                selected = (shippingMethod == selectedShippingMethod),
                                onClick = { onShippingMethodSelected(shippingMethod) },
                                role = Role.RadioButton
                            )
                            .padding(horizontal = 16.dp),
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        RadioButton(
                            selected = (shippingMethod == selectedShippingMethod),
                            onClick = null // null recommended for accessibility with screenreaders
                        )
                        Text(
                            text = convertShippingMethod(shippingMethod),
                            style = MaterialTheme.typography.body1,
                            modifier = Modifier.padding(start = 16.dp)
                        )
                    }
                }
            }

            Row(horizontalArrangement = Arrangement.End, verticalAlignment = Alignment.Bottom,
                modifier = Modifier.fillMaxWidth().padding(8.dp)) {
                Button(onClick = { onOrderClick() }) {
                    Text(text = stringResource(R.string.order_button_label))
                }
            }
        }
    }
}



