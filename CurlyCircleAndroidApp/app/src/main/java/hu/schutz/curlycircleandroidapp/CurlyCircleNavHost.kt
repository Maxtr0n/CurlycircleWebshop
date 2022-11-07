package hu.schutz.curlycircleandroidapp

import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import hu.schutz.curlycircleandroidapp.ui.AccountScreen
import hu.schutz.curlycircleandroidapp.ui.CartScreen
import hu.schutz.curlycircleandroidapp.ui.ShopScreen

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