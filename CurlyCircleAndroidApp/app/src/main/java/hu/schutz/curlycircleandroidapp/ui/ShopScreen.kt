package hu.schutz.curlycircleandroidapp.ui

import androidx.compose.foundation.layout.Column
import androidx.compose.material.Button
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@Composable
fun ShopScreen() {
    Column {
        Text(text = "Shop screen")
        Button(onClick = { /*TODO*/ }) {
            Text(text = "Yes")
        }
    }
}

@Preview
@Composable
fun ShopScreenPreview() {
    CurlyCircleAndroidAppTheme {
        ShopScreen()
    }
}