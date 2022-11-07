package hu.schutz.curlycircleandroidapp.ui

import androidx.compose.foundation.layout.Column
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@Composable
fun AccountScreen() {
    Surface {
        Column {
            Text(text = "Személyes adatok")
        }
    }
}

@Preview
@Composable
fun AccountScreenPreview() {
    CurlyCircleAndroidAppTheme {
        AccountScreen()
    }
}