package hu.schutz.curlycircleandroidapp

import android.content.res.Configuration.UI_MODE_NIGHT_YES
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.padding
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.navigation.compose.rememberNavController
import dagger.hilt.android.AndroidEntryPoint
import hu.schutz.curlycircleandroidapp.ui.CurlyCircleBottomNavigation
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

@AndroidEntryPoint
class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            CurlyCircleApp()
        }
    }
}

@Composable
fun CurlyCircleApp() {
    CurlyCircleAndroidAppTheme {
        val navController = rememberNavController()
        val scaffoldState = rememberScaffoldState()

        Scaffold(
            scaffoldState = scaffoldState,
            bottomBar = { CurlyCircleBottomNavigation(navController = navController) }
        ) { innerPadding ->
            CurlyCircleNavHost(
                navController = navController,
                scaffoldState = scaffoldState,
                modifier = Modifier.padding(innerPadding)
            )
        }
    }
}

@Preview(
    showBackground = true,
    widthDp = 320
)
@Preview(
    showBackground = true,
    widthDp = 320,
    uiMode = UI_MODE_NIGHT_YES,
    name = "Dark"
)
@Composable
fun CurlyCircleAppPreview() {
    CurlyCircleApp()
}
