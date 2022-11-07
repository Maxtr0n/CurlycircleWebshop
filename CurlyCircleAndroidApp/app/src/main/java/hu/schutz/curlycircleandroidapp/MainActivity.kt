package hu.schutz.curlycircleandroidapp

import android.content.res.Configuration.UI_MODE_NIGHT_YES
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.vector.ImageVector
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.navigation.NavDestination.Companion.hierarchy
import androidx.navigation.NavGraph.Companion.findStartDestination
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.compose.rememberNavController
import hu.schutz.curlycircleandroidapp.ui.AccountScreen
import hu.schutz.curlycircleandroidapp.ui.CartScreen
import hu.schutz.curlycircleandroidapp.ui.ShopScreen
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme

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

        Scaffold(
            bottomBar = { CurlyCircleBottomNavigation(navController = navController) }
        ) { innerPadding ->
            CurlyCircleNavHost(
                navController = navController,
                modifier = Modifier.padding(innerPadding)
            )
        }
    }
}


@Composable
fun CurlyCircleNavHost(
    navController: NavHostController,
    modifier: Modifier = Modifier
) {
    NavHost(
        navController = navController,
        startDestination = Screen.Shop.route,
        modifier = modifier
    ) {
        composable(route = Screen.Shop.route) {
            ShopScreen()
        }
        composable(route = Screen.Cart.route) {
            CartScreen()
        }
        composable(route = Screen.Account.route) {
            AccountScreen()
        }
    }
}

@Composable
fun CurlyCircleBottomNavigation(
    navController: NavHostController,
    modifier: Modifier = Modifier,
    ) {
    BottomNavigation(
        modifier = modifier,
    ) {
        val navBackStackEntry by navController.currentBackStackEntryAsState()
        val currentDestination = navBackStackEntry?.destination

       bottomNavScreens.forEach{ screen ->
           BottomNavigationItem(
               icon = { Icon(imageVector = screen.icon, contentDescription = null)},
               label = { Text(stringResource(id = screen.resourceId))},
               selected = currentDestination?.hierarchy?.any { it.route == screen.route} == true,
               onClick = {
                   navController.navigate(screen.route) {
                       // Pop up to the start destination of the graph to
                       // avoid building up a large stack of destinations
                       // on the back stack as users select items
                       popUpTo(navController.graph.findStartDestination().id) {
                           saveState = true
                       }
                       // Avoid multiple copies of the same destination when
                       // reselecting the same item
                       launchSingleTop = true
                       // Restore state when reselecting a previously selected item
                       restoreState = true
                   }
               })
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
