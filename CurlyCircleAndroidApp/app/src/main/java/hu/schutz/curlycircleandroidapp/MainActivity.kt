package hu.schutz.curlycircleandroidapp

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material.BottomNavigation
import androidx.compose.material.BottomNavigationItem
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AccountCircle
import androidx.compose.material.icons.filled.Shop
import androidx.compose.material.icons.filled.ShoppingCart
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            CurlyCircleAndroidAppTheme {
                // A surface container using the 'background' color from the theme
                Surface(
                    modifier = Modifier.fillMaxSize(),
                    color = MaterialTheme.colorScheme.background
                ) {

                }
            }
        }
    }
}


@Composable
fun CurlyCircleNavHost(
    navController: NavHostController,
    modifier: Modifier = Modifier
) {
    /*NavHost(
        navController = navController,
    ) {

    }*/
}

@Composable
fun CurlyCircleBottomNavigation(modifier: Modifier = Modifier) {
    BottomNavigation(
        modifier = modifier
    ) {
        BottomNavigationItem(
            icon = {
                   Icon(imageVector = Icons.Default.Shop, contentDescription = null)
            },
            label = {
                    Text(text = stringResource(R.string.bottom_navigation_shop))
            },
            selected = true,
            onClick = { /*TODO*/ }
        )
        BottomNavigationItem(
            icon = {
                Icon(imageVector = Icons.Default.ShoppingCart, contentDescription = null)
            },
            label = {
                Text(text = stringResource(R.string.bottom_navigation_cart))
            },
            selected = true,
            onClick = { /*TODO*/ }
        )
        BottomNavigationItem(
            icon = {
                Icon(imageVector = Icons.Default.AccountCircle, contentDescription = null)
            },
            label = {
                Text(text = stringResource(R.string.bottom_navigation_account))
            },
            selected = true,
            onClick = { /*TODO*/ }
        )
    }
}