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
    var imageUrls: List<String>,
    var isAvailable: Boolean = true,
    var material: String,
    var pattern: String,
    var colors: List<String>
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

data class ProductViewModel(
    var id: Int,
    var name: String = "",
    var description: String = "",
    var price: Double,
    var productCategoryId: Int,
    var thumbnailImageUrl: String = "",
    var imageUrls: List<String>,
    var isAvailable: Boolean = true,
    var material: Material,
    var pattern: Pattern,
    var colors: List<Color>
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
    var minPrice: Int?,
    var maxPrice: Int?,
    var colorIds: List<Int>,
    var patternIds: List<Int>,
    var materialIds: List<Int>,
    var pageIndex: Int?,
    var pageSize: Int?
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