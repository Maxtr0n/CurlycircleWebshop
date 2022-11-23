package hu.schutz.curlycircleandroidapp.ui.cart

import androidx.annotation.StringRes
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.rememberUpdatedState
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
import hu.schutz.curlycircleandroidapp.data.CartItemAndProduct
import hu.schutz.curlycircleandroidapp.data.Product
import hu.schutz.curlycircleandroidapp.ui.components.LoadingContent
import hu.schutz.curlycircleandroidapp.ui.components.QuantityPicker
import hu.schutz.curlycircleandroidapp.ui.theme.CurlyCircleAndroidAppTheme
import hu.schutz.curlycircleandroidapp.util.Constants

@OptIn(ExperimentalLifecycleComposeApi::class)
@Composable
fun CartScreen(
    @StringRes userMessage: Int,
    onUserMessageDisplayed: () -> Unit,
    scaffoldState : ScaffoldState,
    viewModel: CartViewModel = hiltViewModel(),
    onCheckout: () -> Unit,
) {
    val uiState by viewModel.uiState.collectAsStateWithLifecycle()

    CartContent(
        loading = uiState.isLoading,
        empty = uiState.cartItems.isEmpty() && !uiState.isLoading,
        cartItems = uiState.cartItems,
        clearCart = { viewModel.clearCart() },
        increaseQuantity = { cartItem -> viewModel.increaseQuantity(cartItem) },
        decreaseQuantity = { cartItem -> viewModel.decreaseQuantity(cartItem) },
        removeCartItem = { cartItem -> viewModel.removeCartItem(cartItem) },
        checkoutClick = { onCheckout() }
    )

    uiState.userMessage?.let { message ->
        val snackBarText = stringResource(id = message)
        LaunchedEffect(scaffoldState, viewModel, message, snackBarText) {
            scaffoldState.snackbarHostState.showSnackbar(snackBarText)
            viewModel.snackBarMessageShown()
        }
    }

    val currentOnUserMessageDisplayed by rememberUpdatedState(onUserMessageDisplayed)
    LaunchedEffect(userMessage) {
        if (userMessage != 0) {
            viewModel.showSnackBarMessage(userMessage)
            currentOnUserMessageDisplayed()
        }
    }
}

@Composable
fun CartContent(
    loading: Boolean,
    empty: Boolean,
    cartItems: List<CartItemAndProduct>,
    clearCart: () -> Unit,
    increaseQuantity: (CartItem) -> Unit,
    decreaseQuantity: (CartItem) -> Unit,
    removeCartItem: (CartItem) -> Unit,
    checkoutClick: () -> Unit,
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
        Column(modifier = commonModifier.fillMaxSize()) {
            Row(
                verticalAlignment = Alignment.Bottom,
                horizontalArrangement = Arrangement.SpaceBetween,
                modifier = Modifier.fillMaxWidth()
            ) {
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
                
                Button(onClick = { clearCart() }, colors = ButtonDefaults.buttonColors(backgroundColor = MaterialTheme.colors.error)) {
                    Icon(imageVector = Icons.Default.Delete, contentDescription = "Kosár ürítése.")
                    Text(text = "Kosár ürítése")
                }
            }
            

            LazyColumn(
                verticalArrangement = Arrangement.spacedBy(8.dp),
                contentPadding = PaddingValues(vertical = 8.dp),
                modifier = Modifier.weight(1f)
            ) {
                items(cartItems.toList()) { cartItemAndProduct ->
                    CartItemListItem(
                        cartItem = cartItemAndProduct.cartItem,
                        product = cartItemAndProduct.product,
                        removeCartItem = removeCartItem,
                        increaseQuantity = increaseQuantity,
                        decreaseQuantity = decreaseQuantity
                    )
                }
            }

            Row(horizontalArrangement = Arrangement.End, verticalAlignment = Alignment.Bottom,
                modifier = Modifier.fillMaxWidth().padding(8.dp)) {
                Button(onClick = { checkoutClick() }) {
                    Text(text = stringResource(R.string.order_button_label))
                }
            }
        }
    }
}


@Composable
fun CartItemListItem(
    cartItem: CartItem,
    product: Product,
    removeCartItem: (CartItem) -> Unit,
    increaseQuantity: (CartItem) -> Unit,
    decreaseQuantity: (CartItem) -> Unit
) {
    Card{
        Row(
            modifier = Modifier
                .padding(
                    horizontal = 16.dp,
                    vertical = 8.dp
                ),
            verticalAlignment = Alignment.CenterVertically,
            horizontalArrangement = Arrangement.Center
        ) {
            Text(text = product.name, style = MaterialTheme.typography.body1,
                modifier = Modifier
                    .weight(1f)
                    .padding(4.dp), textAlign = TextAlign.Center)

            QuantityPicker(quantity = cartItem.quantity, increaseQuantity = { increaseQuantity(cartItem) },
             decreaseQuantity = { decreaseQuantity(cartItem) }, modifier = Modifier.weight(1.5f))
            Text(text = "${cartItem.price.toInt()*cartItem.quantity} Ft",
                style = MaterialTheme.typography.body1,
                modifier = Modifier
                    .weight(1f)
                    .padding(4.dp), textAlign = TextAlign.Center)
            IconButton(onClick = { removeCartItem(cartItem) }, modifier = Modifier.weight(0.3f)) {
                Icon(
                    imageVector = Icons.Default.RemoveShoppingCart,
                    contentDescription = "Termék eltávolítása a kosárból",
                    tint = MaterialTheme.colors.error
                )
            }
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
                removeCartItem = {},
                increaseQuantity = {},
                decreaseQuantity = {}
            )
        }
    }
}

@Preview
@Composable
fun CartContentPreview() {
    CurlyCircleAndroidAppTheme {
        Surface {
            CartContent(
                loading = false,
                empty = false,
                cartItems = listOf(
                    CartItemAndProduct(
                        cartItem = CartItem(id = 1, cartId = 1, productId = 1, quantity = 2, price = 2000.0),
                        product = Product(
                            id = 1, name = "Name1", description = "Lorem ipsum 1", price = 2000.0,
                            productCategoryId = 1
                        )
                    )
                ),
                clearCart = { },
                increaseQuantity = {},
                decreaseQuantity = {},
                removeCartItem = {},
                checkoutClick = {}
            )
        }
    }
}