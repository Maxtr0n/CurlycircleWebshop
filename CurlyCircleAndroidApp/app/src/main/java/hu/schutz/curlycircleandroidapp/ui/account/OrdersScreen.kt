package hu.schutz.curlycircleandroidapp.ui.account

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.Order
import hu.schutz.curlycircleandroidapp.ui.components.LoadingContent
import hu.schutz.curlycircleandroidapp.ui.shop.products.ProductItem
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.getCurrentDateTime
import hu.schutz.curlycircleandroidapp.util.toString
import java.text.DateFormat.getDateInstance
import java.text.SimpleDateFormat
import java.time.Instant
import java.util.Date

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun OrdersScreen(
    scaffoldState: ScaffoldState,
    viewModel: OrdersViewModel = hiltViewModel()
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    OrdersContent(
        loading = uiState.isLoading,
        empty = uiState.orders.isEmpty() && !uiState.isLoading,
        orders = uiState.orders
    )

}

@Composable
fun OrdersContent(
    loading: Boolean,
    empty: Boolean,
    orders: List<Order>,
    modifier: Modifier = Modifier
    ) {

    val screenPadding = Modifier.padding(
        horizontal = 16.dp
    )

    val commonModifier = modifier
        .fillMaxWidth()
        .then(screenPadding)

    LoadingContent(
        loading = loading,
        empty = empty,
        emptyContent = {
            Box(
                modifier = commonModifier.fillMaxSize(),
                contentAlignment = Alignment.Center
            ) {
                Text(
                    text = stringResource(R.string.no_data_orders),
                    modifier = Modifier.align(Alignment.Center)
                )
            }
        }
    ) {
        LazyVerticalGrid(
            columns = GridCells.Adaptive(minSize = 256.dp),
            verticalArrangement = Arrangement.spacedBy(8.dp),
            horizontalArrangement = Arrangement.spacedBy(8.dp),
            contentPadding = PaddingValues(vertical = 8.dp),
            modifier = Modifier.fillMaxSize()
        ) {
            items(orders) { order ->
                OrderListItem(
                    order = order
                )
            }
        }
    }
}

@Composable
fun OrderListItem(
    order: Order
) {
    Card(modifier = Modifier) {
        Column(
            modifier = Modifier
                .padding(
                    horizontal = 16.dp,
                    vertical = 8.dp
                ),
            horizontalAlignment = Alignment.Start
        ) {

            Text(
                text = order.orderDateTime.toString(format = "yyyy. MM. dd. HH:mm:ss"),
                style = MaterialTheme.typography.h5,
                modifier = Modifier.padding(bottom = 8.dp)
            )
            Text(
                text = "Összesen: ${order.total.toInt()} Ft",
                modifier = Modifier.padding(bottom = 8.dp)
            )
            Text(text = "Megjegyzés: ${order.note}")
        }
    }
}

@Preview
@Composable
private fun OrderListItemPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            OrderListItem(
                order = Order(
                    id = 1,
                    orderDateTime = getCurrentDateTime(),
                    total = 2500.0,
                    note = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum."
                )
            )
        }
    }
}

@Preview
@Composable
private fun OrdersContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            OrdersContent(
                loading = false,
                empty = false,
                orders = listOf(
                    Order(
                        id = 1,
                        orderDateTime = getCurrentDateTime(),
                        total = 2500.0,
                        note = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum."
                    ),
                    Order(
                        id = 2,
                        orderDateTime = getCurrentDateTime(),
                        total = 1500.0,
                        note = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum."
                    ),
                    Order(
                        id = 2,
                        orderDateTime = getCurrentDateTime(),
                        total = 5000.0,
                        note = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum."
                    ),
                    Order(
                        id = 2,
                        orderDateTime = getCurrentDateTime(),
                        total = 3000.0,
                        note = "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum."
                    ),
                )
            )
        }
    }
}
