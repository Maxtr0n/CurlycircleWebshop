package hu.schutz.curlycircleandroidapp.ui.account

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.ClickableText
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Visibility
import androidx.compose.material.icons.filled.VisibilityOff
import androidx.compose.runtime.*
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.AnnotatedString
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.text.input.VisualTransformation
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.User
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun AccountScreen(
    scaffoldState: ScaffoldState,
    viewModel: AccountViewModel = hiltViewModel(),
    onRegisterClick: () -> Unit,
    onLoginClick: () -> Unit = viewModel::login
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    AccountContent(
        loading = uiState.isLoading,
        user = uiState.user,
        onLoginClick = onLoginClick,
        onRegisterClick = onRegisterClick
    )

    uiState.userMessage?.let { message ->
        val snackBarText = stringResource(id = message)
        LaunchedEffect(scaffoldState, viewModel, message, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackBarMessageShown()
        }
    }
}


@Composable
fun AccountContent(
    loading: Boolean,
    user: User?,
    onRegisterClick: () -> Unit,
    onLoginClick: () -> Unit,
    modifier: Modifier = Modifier
    ) {
    if (user == null) {
        AnonymousContent(
            onRegisterClick = onRegisterClick,
            onLoginClick = onLoginClick
        )
    } else {
        LoggedInContent()
    }
}


@Composable
fun LoggedInContent() {

}

@Composable
fun AnonymousContent(
    onLoginClick: () -> Unit,
    onRegisterClick: () -> Unit,
    modifier: Modifier = Modifier
) {
        Column(
            modifier = modifier
                .fillMaxSize()
                .padding(16.dp),
            verticalArrangement = Arrangement.Center,
            horizontalAlignment = Alignment.CenterHorizontally
        ) {

            val email = remember { mutableStateOf("") }
            val password = remember { mutableStateOf("") }

            Text(text = stringResource(R.string.login_label),
                style = MaterialTheme.typography.h4,
            )

            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.email_address)) },
                value = email.value,
                onValueChange = { email.value = it })

            Spacer(modifier = Modifier.height(20.dp))
            PasswordTextField(value = password.value, onValueChange = { password.value = it })
            Spacer(modifier = Modifier.height(20.dp))
                Button(
                    onClick = {
                        onLoginClick()
                    }
                ) {
                    Text(text = stringResource(R.string.login_button_text))
                }


            ClickableText(
                text = AnnotatedString(stringResource(R.string.register_here)),
                modifier = Modifier
                    .padding(20.dp),
                onClick = {
                    onRegisterClick()
                },
                style = TextStyle(
                    fontSize = 14.sp,
                    fontFamily = FontFamily.Default,
                    textDecoration = TextDecoration.Underline,
                    color = MaterialTheme.colors.primary
                )
            )
        }
}

@Composable
fun PasswordTextField(
    modifier: Modifier = Modifier,
    value: String,
    onValueChange: (String) -> Unit = {},
) {
    var passwordVisible by rememberSaveable { mutableStateOf(false) }

    TextField(
        modifier = modifier,
        value = value,
        onValueChange = onValueChange,
        keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Password),
        visualTransformation = if (passwordVisible) VisualTransformation.None
        else PasswordVisualTransformation(),
        trailingIcon = {
            val image = if (passwordVisible)
                Icons.Filled.Visibility
            else Icons.Filled.VisibilityOff

            val description = if (passwordVisible) "Jelszó elrejtése" else "Jelszó megjelenítése"

            IconButton(onClick = {passwordVisible = !passwordVisible}){
                Icon(imageVector  = image, description)
            }
        },
        label = { Text(text = stringResource(R.string.password)) },
    )
}


@Preview
@Composable
fun AnonymousPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            AnonymousContent(
                onLoginClick = {},
                onRegisterClick = {}
            )
        }
    }
}