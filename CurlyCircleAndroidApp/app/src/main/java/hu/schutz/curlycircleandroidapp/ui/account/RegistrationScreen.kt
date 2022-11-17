package hu.schutz.curlycircleandroidapp.ui.account

import android.widget.Space
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun RegistrationScreen(
    viewModel: RegistrationViewModel = hiltViewModel(),
    scaffoldState: ScaffoldState,
    onBackClick: () -> Unit,
    onSuccessfulRegistration: () -> Unit,
    onRegisterClick: () -> Unit = viewModel::register
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    RegistrationContent(
        loading = uiState.isLoading,
        email = uiState.email,
        password = uiState.password,
        firstName = uiState.firstName,
        lastName = uiState.lastName,
        city = uiState.city,
        zipCode = uiState.zipCode,
        line1 = uiState.line1,
        line2 = uiState.line2,
        phoneNumber = uiState.phoneNumber,
        onEmailChanged = viewModel::updateEmail,
        onPasswordChanged = viewModel::updatePassword,
        onFirstNameChanged = viewModel::updateFirstName,
        onLastNameChanged = viewModel::updateLastName,
        onCityChanged = viewModel::updateCity,
        onZipCodeChanged = viewModel::updateZipCode,
        onLine1Changed = viewModel::updateLine1,
        onLine2Changed = viewModel::updateLine2,
        onPhoneNumberChanged = viewModel::updatePhoneNumber
    )

    LaunchedEffect(uiState.registrationSuccessful) {
        if (uiState.registrationSuccessful) {
            onSuccessfulRegistration()
        }
    }

    // Check for user messages to display on the screen
    uiState.userMessage?.let { userMessage ->
        val snackbarText = stringResource(userMessage)
        LaunchedEffect(scaffoldState, viewModel, userMessage, snackbarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackbarText)
            viewModel.snackBarMessageShown()
        }
    }
}


@Composable
fun RegistrationContent(
    loading: Boolean,
    email: String,
    password: String,
    firstName: String,
    lastName: String,
    city: String,
    zipCode: String,
    line1: String,
    line2: String,
    phoneNumber: String,
    onEmailChanged: (String) -> Unit,
    onPasswordChanged: (String) -> Unit,
    onFirstNameChanged: (String) -> Unit,
    onLastNameChanged: (String) -> Unit,
    onCityChanged: (String) -> Unit,
    onZipCodeChanged: (String) -> Unit,
    onLine1Changed: (String) -> Unit,
    onLine2Changed: (String) -> Unit,
    onPhoneNumberChanged: (String) -> Unit,
    modifier: Modifier = Modifier
) {
    if (loading) {

    } else {
        Column(
            modifier = modifier
                .fillMaxSize()
                .padding(16.dp)
                .verticalScroll(rememberScrollState()),
            verticalArrangement = Arrangement.Center,
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(
                text = stringResource(id = R.string.registration),
                style = MaterialTheme.typography.h5
            )
            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.email_address)) },
                value = email,
                onValueChange = onEmailChanged
            )
            Spacer(modifier = Modifier.height(20.dp))
            PasswordTextField(value = password, onValueChange = onPasswordChanged)
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
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceAround
            ) {
                OutlinedButton(
                    onClick = { /*TODO*/ }
                ) {
                    Text(text = "Vissza")
                }
                Button(
                    onClick = { /*TODO*/ }
                ) {
                    Text(text = "Regisztrálok")
                }
            }
        }
    }
}

/*
@Preview
@Composable
fun RegistrationContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            RegistrationContent()
        }
    }
}
 */