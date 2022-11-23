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
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.CART_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.ORDER_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.PRODUCTS_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.PRODUCT_CATEGORIES_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleDestinations.PRODUCT_DETAILS_ROUTE
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.PRODUCT_CATEGORY_ID_ARG
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.PRODUCT_ID_ARG
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_ID_ARG
import hu.schutz.curlycircleandroidapp.CurlyCircleNavigationArgs.USER_MESSAGE_ARG
import hu.schutz.curlycircleandroidapp.ui.account.AccountScreen
import hu.schutz.curlycircleandroidapp.ui.account.OrdersScreen
import hu.schutz.curlycircleandroidapp.ui.account.RegistrationScreen
import hu.schutz.curlycircleandroidapp.ui.cart.CartScreen
import hu.schutz.curlycircleandroidapp.ui.cart.OrderScreen
import hu.schutz.curlycircleandroidapp.ui.shop.productcategories.ProductCategoriesScreen
import hu.schutz.curlycircleandroidapp.ui.shop.productdetails.ProductDetailsScreen
import hu.schutz.curlycircleandroidapp.ui.shop.products.ProductsScreen

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
        navigation(
            route = BottomNavScreen.Shop.route,
            startDestination = PRODUCT_CATEGORIES_ROUTE
        ) {

            composable(
                route = PRODUCT_CATEGORIES_ROUTE
            ) {
                ProductCategoriesScreen(
                    scaffoldState = scaffoldState,
                    onProductCategoryClick = { id -> navActions.navigateToProductsScreen(id) }
                )
            }

            composable(
                route = PRODUCTS_ROUTE,
                arguments = listOf(
                    navArgument(PRODUCT_CATEGORY_ID_ARG) { type = NavType.IntType }
                )
            ) { backStackEntry ->
                ProductsScreen(
                    scaffoldState = scaffoldState,
                    onProductClick = { id -> navActions.navigateToProductDetailsScreen(id) }
                )
            }

            composable(
                route = PRODUCT_DETAILS_ROUTE,
                arguments = listOf(
                    navArgument(PRODUCT_ID_ARG) { type = NavType.IntType }
                )
            ) { backStackEntry ->
                ProductDetailsScreen(
                    scaffoldState = scaffoldState,
                )
            }
        }

        navigation(
            route = BottomNavScreen.CartAndOrder.route,
            startDestination = CART_ROUTE
        ) {
            composable(route = CART_ROUTE) {
                CartScreen(
                    scaffoldState = scaffoldState,
                    onCheckout = { navActions.navigateToOrderScreen() }
                )
            }

            composable(route = ORDER_ROUTE) {
                OrderScreen()
            }
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
                    onRegisterClick = { navActions.navigateToRegistrationScreen() },
                    onOrdersClicked = { userId -> navActions.navigateToOrdersScreen(userId)}
                )
            }
            composable(route = CurlyCircleDestinations.REGISTRATION_ROUTE) {
                RegistrationScreen(
                    onBackClick = { navActions.navigateBackToAccountScreen() },
                    onSuccessfulRegistration = { navActions.navigateBackToAccountScreen(R.string.registration_successful) },
                    scaffoldState = scaffoldState
                )
            }
            composable(
                route = CurlyCircleDestinations.ORDERS_ROUTE,
                arguments = listOf(
                    navArgument(USER_ID_ARG) { type = NavType.IntType }
                )
            ) {
                OrdersScreen(scaffoldState = scaffoldState)
            }
        }
    }
}