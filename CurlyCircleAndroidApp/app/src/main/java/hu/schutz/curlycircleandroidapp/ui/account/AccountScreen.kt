package hu.schutz.curlycircleandroidapp.ui

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import hu.schutz.curlycircleandroidapp.ui.account.AccountViewModel
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@Composable
fun AccountScreen(
    viewModel: AccountViewModel = hiltViewModel()
) {

}



@Composable
fun LoggedInContent() {

}

@Composable
fun AnonymousContent(
    modifier: Modifier = Modifier
) {
    Column(
        modifier = modifier.fillMaxSize()
    ) {

    }
}


@Preview
@Composable
fun AnonymousPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            AnonymousContent()
        }
    }
}