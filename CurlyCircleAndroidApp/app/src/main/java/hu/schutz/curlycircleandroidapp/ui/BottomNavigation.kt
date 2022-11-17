package hu.schutz.curlycircleandroidapp.ui

import androidx.compose.material.BottomNavigation
import androidx.compose.material.BottomNavigationItem
import androidx.compose.material.Icon
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.navigation.NavDestination
import androidx.navigation.NavDestination.Companion.hierarchy
import hu.schutz.curlycircleandroidapp.BottomNavScreen
import hu.schutz.curlycircleandroidapp.bottomNavScreens

@Composable
fun CurlyCircleBottomNavigation(
    currentDestination: NavDestination?,
    onNavigationItemClick: (BottomNavScreen) -> Unit,
    modifier: Modifier = Modifier,
) {
    BottomNavigation(
        modifier = modifier,
    ) {

        bottomNavScreens.forEach{ screen ->
            BottomNavigationItem(
                icon = { Icon(imageVector = screen.icon, contentDescription = null) },
                label = { Text(stringResource(id = screen.resourceId)) },
                selected = currentDestination?.hierarchy?.any { it.route == screen.route} == true,
                onClick = { onNavigationItemClick(screen) }
            )
        }
    }
}