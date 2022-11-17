package hu.schutz.curlycircleandroidapp

import androidx.compose.material.ScaffoldState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import hu.schutz.curlycircleandroidapp.ui.account.AccountScreen
import hu.schutz.curlycircleandroidapp.ui.account.RegistrationScreen
import hu.schutz.curlycircleandroidapp.ui.cart.CartScreen
import hu.schutz.curlycircleandroidapp.ui.shop.ShopScreen

@Composable
fun CurlyCircleNavHost(
    navController: NavHostController,
    scaffoldState: ScaffoldState,
    modifier: Modifier = Modifier,
    navActions: CurlyCircleNavigationActions = remember(navController) {
        CurlyCircleNavigationActions(navController)
    }
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
            CartScreen(
                scaffoldState = scaffoldState
            )
        }
        composable(route = BottomNavScreen.Account.route) {
            AccountScreen(
                scaffoldState = scaffoldState,
                onRegisterClick = { navActions.navigateToRegistrationScreen() }
            )
        }
        composable(route = CurlyCircleDestinations.REGISTRATION_ROUTE) {
            RegistrationScreen(
                onBackClick = { navActions.navigateBackToAccountScreen() },
                onSuccessfulRegistration = { navActions.navigateBackToAccountScreen() },
                scaffoldState = scaffoldState
            )
        }
    }
}