package hu.schutz.curlycircleandroidapp.data

import androidx.room.Entity
import androidx.room.PrimaryKey

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
    @PrimaryKey var id: Int,
    var cartId: Int,
    var email: String = "",
    var firstName: String = "",
    var lastName: String = "",
    var city: String = "",
    var zipCode: String = "",
    var line1: String = "",
    var line2: String?,
    var phoneNumber: String = "",
    var accessToken: String = "",
    var refreshToken: String = "",
    var role: Role
)

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

enum class Role {
    User,
    Admin
}
