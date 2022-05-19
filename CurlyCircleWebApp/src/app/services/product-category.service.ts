import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { ProductCategoriesViewModel, ProductsViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductCategoryService {
    private readonly productCategoriesUrl = 'api/productcategories';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getProductCategories(): Observable<ProductCategoriesViewModel> {
        return this.httpClient.get<ProductCategoriesViewModel>(`${this.productCategoriesUrl}`);
    }

    public getProductCategoryProducts(productCategoryId: number): Observable<ProductsViewModel> {
        return this.httpClient.get<ProductsViewModel>(`${this.productCategoriesUrl}/${productCategoryId}/products`);
    }
}
