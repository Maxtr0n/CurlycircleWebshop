import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, ProductCategoriesViewModel, ProductCategoryUpsertDto, ProductCategoryViewModel, ProductsViewModel, ProductUpsertDto } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductCategoryService {
    private readonly productCategoriesApiUrl = 'api/productcategories';

    constructor(private readonly httpClient: AppHttpClient) { }

    getProductCategories(): Observable<ProductCategoriesViewModel> {
        return this.httpClient.get<ProductCategoriesViewModel>(`${this.productCategoriesApiUrl}`);
    }

    getProductCategory(productcategoryId: number): Observable<ProductCategoryViewModel> {
        return this.httpClient.get<ProductCategoryViewModel>(`${this.productCategoriesApiUrl}/${productcategoryId}`);
    }

    getProductCategoryProducts(productcategoryId: number): Observable<ProductsViewModel> {
        return this.httpClient.get<ProductsViewModel>(`${this.productCategoriesApiUrl}/${productcategoryId}/products`);
    }

    updateProductCategory(productcategoryId: number, dto: ProductCategoryUpsertDto): Observable<void> {
        return this.httpClient.put(`${this.productCategoriesApiUrl}/${productcategoryId}`, dto);
    }

    addProductCategory(dto: ProductCategoryUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productCategoriesApiUrl}`, dto);
    }

    addProduct(productcategoryId: number, dto: ProductUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productCategoriesApiUrl}/${productcategoryId}/products`, dto);
    }

    deleteProductCategory(productcategoryId: number): Observable<void> {
        return this.httpClient.delete(`${this.productCategoriesApiUrl}/${productcategoryId}`);
    }
}
