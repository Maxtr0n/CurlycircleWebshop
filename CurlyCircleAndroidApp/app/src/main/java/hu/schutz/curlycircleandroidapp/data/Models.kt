package hu.schutz.curlycircleandroidapp.data

import androidx.room.Entity
import androidx.room.PrimaryKey
import androidx.room.TypeConverters
import hu.schutz.curlycircleandroidapp.util.Converters
import java.util.Date

@Entity(tableName = "productCategories")
data class ProductCategory(
    var name: String = "",
    var description: String = "",
    var thumbnailImageUrl: String = "",
    var isAvailable: Boolean = true,
    @PrimaryKey var id: Int
    )

data class ProductCategoriesViewModel(
    var productCategories: List<ProductCategory>
)

@Entity(tableName = "products")
data class Product(
    @PrimaryKey var id: Int,
    var name: String = "",
    var description: String = "",
    var price: Double,
    var productCategoryId: Int,
    var thumbnailImageUrl: String = "",
    var imageUrls: List<String> = emptyList(),
    var isAvailable: Boolean = true,
    var material: String = "",
    var pattern: String = "",
    var colors: List<String> = emptyList()
)

@Entity(tableName = "colors")
data class Color(
    @PrimaryKey var id: Int,
    var name: String = "",
)

@Entity(tableName = "patterns")
data class Pattern(
    @PrimaryKey var id: Int,
    var name: String = "",
)

@Entity(tableName = "materials")
data class Material(
    @PrimaryKey var id: Int,
    var name: String = "",
)

@Entity(tableName = "user")
data class User(
    @PrimaryKey var databaseId: Int,
    var id: Int,
    var cartId: Int,
    var email: String = "",
    var firstName: String = "",
    var lastName: String = "",
    var city: String = "",
    var zipCode: String = "",
    var line1: String = "",
    var line2: String? = null,
    var phoneNumber: String = "",
    var accessToken: String = "",
    var refreshToken: String = "",
    var role: Role = Role.User
)

@Entity(tableName = "orders")
data class Order(
    @PrimaryKey var id: Int,
    var orderDateTime: Date,
    var total: Double,
    var note: String? = null,
    var email: String = "",
    var firstName: String = "",
    var lastName: String = "",
    var city: String = "",
    var zipCode: String = "",
    var line1: String = "",
    var line2: String? = null,
    var phoneNumber: String = "",
    var shippingMethod: ShippingMethod = ShippingMethod.Foxpost,
    var paymentMethod: PaymentMethod = PaymentMethod.CashOnDelivery
)

@Entity(tableName = "cart")
data class Cart(
    @PrimaryKey var databaseId: Int,
    var id: Int
)

@Entity(tableName = "cartItems")
data class CartItem(
    @PrimaryKey var id: Int,
    var cartId: Int,
    var productId: Int,
    var price: Double,
    var quantity: Int
)

data class CartViewModel(
    var id: Int,
    var cartItems: List<CartItemViewModel>
)

data class CartItemViewModel(
    var id: Int,
    var cartId: Int,
    var productId: Int,
    var product: ProductViewModel,
    var price: Double,
    var quantity: Int
)

data class CartItemUpsertDto(
    var productId: Int,
    var price: Double,
    var quantity: Int
)

/*
data class OrderItem(
    @PrimaryKey var id: Int,
    var orderId: Int,
    var productId: Int,
    var price: Double,
    var quantity: Int
)

 */

data class ProductViewModel(
    var id: Int,
    var name: String = "",
    var description: String = "",
    var price: Double,
    var productCategoryId: Int,
    var thumbnailImageUrl: String = "",
    var imageUrls: List<String> = emptyList(),
    var isAvailable: Boolean = true,
    var material: Material? = null,
    var pattern: Pattern? = null,
    var colors: List<Color> = emptyList()
)

data class PagedProductsViewModel(
    var products: List<ProductViewModel>,
    var pageIndex: Int,
    var totalPages: Int,
    var pageSize: Int,
    var totalCount: Int,
    var hasPreviousPage: Boolean,
    var hasNextPage: Boolean
)

data class ProductQueryParameters(
    var productCategoryId: Int,
    var minPrice: Int? = null,
    var maxPrice: Int? = null,
    var colorIds: List<Int> = emptyList(),
    var patternIds: List<Int> = emptyList(),
    var materialIds: List<Int> = emptyList(),
    var pageIndex: Int? = null,
    var pageSize: Int? = null
)

data class ColorsViewModel(
    var colors: List<Color>
)

data class PatternsViewModel(
    var patterns: List<Pattern>
)

data class MaterialsViewModel(
    var materials: List<Material>
)

data class LoginDto(
    var email: String,
    var password: String,
    var cartId: Int?
)

data class RegisterDto(
    var email: String,
    var password: String,
    var firstName: String,
    var lastName: String,
    var city: String,
    var zipCode: String,
    var line1: String,
    var line2: String?,
    var phoneNumber: String,
    )

data class RefreshDto(
    var email: String,
    var id: Int,
    var accessToken: String,
    var refreshToken: String
)

data class UserViewModel(
    var id: Int,
    var cartId: Int,
    var email: String,
    var firstName: String,
    var lastName: String,
    var city: String,
    var zipCode: String,
    var line1: String,
    var line2: String?,
    var phoneNumber: String,
    var accessToken: String,
    var refreshToken: String,
    var role: Role
)

data class TokenViewModel(
    var accessToken: String,
    var refreshToken: String
)

data class UserUpdateDto(
    var userId: Int,
    var firstName: String,
    var lastName: String,
    var city: String,
    var zipCode: String,
    var line1: String,
    var line2: String?,
    var phoneNumber: String,
)

data class EntityCreatedViewModel(
    var id: String
)

data class UserDataViewModel(
    var email: String,
    var firstName: String,
    var lastName: String,
    var city: String,
    var zipCode: String,
    var line1: String,
    var line2: String?,
    var phoneNumber: String,
)

data class ChangePasswordDto(
    var email: String,
    var oldPassword: String,
    var newPassword: String
)

data class OrdersViewModel(
    var orders: List<Order>
)

data class OrderUpsertDto(
    var cartId: Int,
    var applicationUserId: Int? = null,
    var note: String? = null,
    var email: String,
    var firstName: String ,
    var lastName: String,
    var city: String,
    var zipCode: String,
    var line1: String,
    var line2: String? = null,
    var phoneNumber: String,
    var shippingMethod: ShippingMethod,
    var paymentMethod: PaymentMethod
)

@TypeConverters(Converters::class)
enum class Role {
    User,
    Admin
}

@TypeConverters(Converters::class)
enum class ShippingMethod {
    Foxpost,
    MagyarPostaPont,
    MagyarPostaCsomagPont,
    HomeDelivery,
    PersonalDelivery
}

@TypeConverters(Converters::class)
enum class PaymentMethod {
    MoneyTransfer,
    CashOnDelivery,
    WebPayment
}

data class ErrorViewModel(
    var message: String
)
