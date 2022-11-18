package hu.schutz.curlycircleandroidapp

import androidx.compose.material.ScaffoldState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.navigation.NavHostController
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.navArgument
import androidx.navigation.navigation
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.ACCOUNT_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_MESSAGE_ARG
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
        navigation(
            route = BottomNavScreen.Profile.route,
            startDestination = ACCOUNT_ROUTE
        ) {
            composable(
                route = ACCOUNT_ROUTE,
                arguments = listOf(
                    navArgument(USER_MESSAGE_ARG) { type = NavType.IntType; defaultValue = 0}
                )
            ) { entry ->
                AccountScreen(
                    userMessage = entry.arguments?.getInt(USER_MESSAGE_ARG)!!,
                    onUserMessageDisplayed = { entry.arguments?.putInt(USER_MESSAGE_ARG, 0) },
                    scaffoldState = scaffoldState,
                    onRegisterClick = { navActions.navigateToRegistrationScreen() }
                )
            }
            composable(route = CurlyCircleDestinations.REGISTRATION_ROUTE) {
                RegistrationScreen(
                    onBackClick = { navActions.navigateBackToAccountScreen() },
                    onSuccessfulRegistration = { navActions.navigateBackToAccountScreen(R.string.registration_successful) },
                    scaffoldState = scaffoldState
                )
            }
        }
    }
}