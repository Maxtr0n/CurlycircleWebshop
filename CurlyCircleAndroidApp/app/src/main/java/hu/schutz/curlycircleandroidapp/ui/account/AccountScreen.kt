package hu.schutz.curlycircleandroidapp.ui.account

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.ClickableText
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.AnnotatedString
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.User
import hu.schutz.curlycircleandroidapp.ui.components.LoadingContent
import hu.schutz.curlycircleandroidapp.ui.components.PasswordTextField
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun AccountScreen(
    scaffoldState: ScaffoldState,
    viewModel: AccountViewModel = hiltViewModel(),
    onRegisterClick: () -> Unit,
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    AccountContent(
        uiState = uiState,
        onLoginClick = { viewModel.login() },
        onLogoutClick = { viewModel.logout() },
        onEmailChanged = { viewModel.updateEmail(it) },
        onPasswordChanged = { viewModel.updatePassword(it) },
        onRegisterClick = onRegisterClick,
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
    uiState: AccountUiState,
    onRegisterClick: () -> Unit,
    onLoginClick: () -> Unit,
    onLogoutClick: () -> Unit,
    onEmailChanged: (String) -> Unit,
    onPasswordChanged: (String) -> Unit,
    modifier: Modifier = Modifier
    ) {

    LoadingContent(
        loading = uiState.isLoading,
        empty = false,
        emptyContent = {}) {
        when (uiState) {
            is AccountUiState.HasUser -> LoggedInContent(
                user = uiState.user,
                onLogoutClick = onLogoutClick,
                modifier = modifier
            )
            is AccountUiState.NoUser -> AnonymousContent(
                email = uiState.email,
                password = uiState.password,
                onLoginClick = onLoginClick,
                onRegisterClick = onRegisterClick,
                onPasswordChanged = onPasswordChanged,
                onEmailChanged = onEmailChanged,
                modifier = modifier
            )
        }
    }
}


@Composable
fun LoggedInContent(
    user: User,
    onLogoutClick: () -> Unit,
    modifier: Modifier = Modifier
    ) {
    Column(
        modifier = modifier
            .fillMaxSize()
            .padding(16.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(text = user.email)
        Button(onClick = onLogoutClick) {
            Text(text = stringResource(R.string.logout_button_label))
        }
    }
}

@Composable
fun AnonymousContent(
    onLoginClick: () -> Unit,
    onRegisterClick: () -> Unit,
    onEmailChanged: (String) -> Unit,
    onPasswordChanged: (String) -> Unit,
    email: String,
    password: String,
    modifier: Modifier = Modifier
) {
        Column(
            modifier = modifier
                .fillMaxSize()
                .padding(16.dp),
            verticalArrangement = Arrangement.Center,
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(text = stringResource(R.string.login_label),
                style = MaterialTheme.typography.h4,
            )

            Spacer(modifier = Modifier.height(20.dp))
            TextField(
                label = { Text(text = stringResource(R.string.email_address)) },
                value = email,
                onValueChange = onEmailChanged)

            Spacer(modifier = Modifier.height(20.dp))
            PasswordTextField(value = password, onValueChange = onPasswordChanged)
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

@Preview
@Composable
fun AnonymousPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            AnonymousContent(
                email = "",
                password = "",
                onLoginClick = {},
                onRegisterClick = {},
                onPasswordChanged = {},
                onEmailChanged = {}
            )
        }
    }
}

@Preview
@Composable
fun LoggedInPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            LoggedInContent(
                user = User(id = 1, firstName = "Máté", lastName = "Kovács", cartId = 1,
                        databaseId = 1, email = "example@gmail.com"),
                onLogoutClick = {}
            )
        }
    }
}