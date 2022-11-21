package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.layout.VerticalAlignmentLine
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.ExperimentalLifecycleComposeApi
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import coil.compose.AsyncImage
import hu.schutz.curlycircleandroidapp.R
import hu.schutz.curlycircleandroidapp.data.CartItem
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.ui.components.LoadingContent
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.Constants

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun CartScreen(
    scaffoldState : ScaffoldState,
    viewModel: CartViewModel = hiltViewModel()
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    CartContent(
        loading = uiState.isLoading,
        empty = uiState.cartItems.isEmpty() && !uiState.isLoading,
        cartItems = uiState.cartItems
    )

    uiState.userMessage?.let { message ->
        val snackBarText = stringResource(id = message)
        LaunchedEffect(scaffoldState, viewModel, message, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackBarMessageShown()
        }
    }
}

@Composable
fun CartContent(
    loading: Boolean,
    empty: Boolean,
    cartItems: Map<CartItem, Product>,
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
                    text = stringResource(R.string.no_data_cart),
                    modifier = Modifier.align(Alignment.Center)
                )
            }
        }) {
        Column(modifier = commonModifier) {
            Text(
                text = stringResource(id = R.string.cart_label),
                modifier = Modifier.padding(
                    top = 16.dp,
                    bottom = 8.dp,
                    start = 8.dp,
                    end = 8.dp
                ),
                style = MaterialTheme.typography.h5
            )

            LazyColumn(
                verticalArrangement = Arrangement.spacedBy(8.dp),
                contentPadding = PaddingValues(vertical = 8.dp),
                modifier = Modifier.fillMaxSize()
            ) {
                items(cartItems.toList()) { cartItemAndProduct ->
                    CartItemListItem(
                        cartItem = cartItemAndProduct.first,
                        product = cartItemAndProduct.second
                    )
                }
            }
        }
    }
}


@Composable
fun CartItemListItem(
    cartItem: CartItem,
    product: Product
) {
    Card{
        Row(
            modifier = Modifier
                .padding(
                    horizontal = 16.dp,
                    vertical = 8.dp
                ),
            verticalAlignment = Alignment.CenterVertically
        ) {
            AsyncImage(
                model = if (product.thumbnailImageUrl == "") Constants.API_BASE_URL
                        + Constants.NO_IMAGE_URL
                else Constants.API_BASE_URL + Constants.PRODUCT_THUMBNAILS_URL +
                        product.thumbnailImageUrl,
                contentDescription = "${product.name} termék képe.",
                placeholder = painterResource(id = R.drawable.placeholder3),
                error = painterResource(id = R.drawable.placeholder3),
                fallback = painterResource(id = R.drawable.placeholder3),
                modifier = Modifier
                    .padding(vertical = 8.dp)
                    .clip(MaterialTheme.shapes.medium)
                    .weight(1f)
            )
            Text(text = product.name, style = MaterialTheme.typography.h5,
                modifier = Modifier
                    .weight(1f)
                    .padding(4.dp), textAlign = TextAlign.Center)
            Text(text = "${cartItem.quantity} x ${cartItem.price.toInt()} Ft",
                style = MaterialTheme.typography.h5,
                modifier = Modifier
                    .weight(1.8f)
                    .padding(4.dp), textAlign = TextAlign.Center)
        }
    }
}

@Preview
@Composable
fun CartItemListItemPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            CartItemListItem(
                cartItem = CartItem(id = 1, cartId = 1, productId = 1, quantity = 2, price = 2000.0),
                product = Product(
                    id = 1, name = "Name1", description = "Lorem ipsum 1", price = 2000.0,
                    productCategoryId = 1
                ),
            )
        }
    }
}