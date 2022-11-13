package hu.schutz.curlycircleandroidapp

import android.content.res.Configuration.UI_MODE_NIGHT_YES
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.padding
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.navigation.NavGraph.Companion.findStartDestination
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.compose.rememberNavController
import dagger.hilt.android.AndroidEntryPoint
import hu.schutz.curlycircleandroidapp.ui.CurlyCircleBottomNavigation
import hu.schutz.curlycircleandroidapp.ui.CurlyCircleTopAppBar
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

        val navBackStackEntry by navController.currentBackStackEntryAsState()
        val currentDestination = navBackStackEntry?.destination

        Scaffold(
            scaffoldState = scaffoldState,
            /*topBar = { CurlyCircleTopAppBar(
                canNavigateBack = navController.previousBackStackEntry != null,
                navigateUp = { navController.navigateUp() }
            ) },*/
            bottomBar = {
                CurlyCircleBottomNavigation(
                    currentDestination = currentDestination,
                    onNavigationItemClick = { screen ->
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
                    }
                )
            }
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
