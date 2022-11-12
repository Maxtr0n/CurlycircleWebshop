package hu.schutz.curlycircleandroidapp

import androidx.compose.material.ScaffoldState
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import hu.schutz.curlycircleandroidapp.ui.AccountScreen
import hu.schutz.curlycircleandroidapp.ui.CartScreen
import hu.schutz.curlycircleandroidapp.ui.shop.ShopScreen

@Composable
fun CurlyCircleNavHost(
    navController: NavHostController,
    scaffoldState: ScaffoldState,
    modifier: Modifier = Modifier
) {
    NavHost(
        navController = navController,
        startDestination = BottomNavScreen.Shop.route,
        modifier = modifier
    ) {
        composable(route = BottomNavScreen.Shop.route) {
            ShopScreen(
                scaffoldState = scaffoldState
            )
        }
        composable(route = BottomNavScreen.Cart.route) {
            CartScreen()
        }
        composable(route = BottomNavScreen.Account.route) {
            AccountScreen()
        }
    }
}