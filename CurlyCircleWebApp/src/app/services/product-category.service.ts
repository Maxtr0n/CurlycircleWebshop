import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, ProductCategoriesViewModel, ProductCategoryUpsertDto, ProductCategoryViewModel, ProductsViewModel, ProductUpsertDto } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductCategoryService {
    private readonly productCategoriesUrl = 'api/productcategory';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getProductCategories(): Observable<ProductCategoriesViewModel> {
        return this.httpClient.get<ProductCategoriesViewModel>(`${this.productCategoriesUrl}`);
    }

    public getProductCategory(productCategoryId: number): Observable<ProductCategoryViewModel> {
        return this.httpClient.get<ProductCategoryViewModel>(`${this.productCategoriesUrl}/${productCategoryId}`);
    }

    public getProductCategoryProducts(productCategoryId: number): Observable<ProductsViewModel> {
        return this.httpClient.get<ProductsViewModel>(`${this.productCategoriesUrl}/${productCategoryId}/products`);
    }

    public deleteProductCategory(productCategoryId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.productCategoriesUrl}/${productCategoryId}`);
    }

    public createProductCategory(productCategory: ProductCategoryUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productCategoriesUrl}`, productCategory);
    }

    public createProduct(productCategoryId: number, product: ProductUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productCategoriesUrl}/${productCategoryId}`, product);
    }
}
