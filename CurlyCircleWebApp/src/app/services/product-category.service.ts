import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, ProductCategoriesViewModel, ProductCategoryUpsertDto, ProductCategoryViewModel, ProductCategoryWithThumbnail, ProductsViewModel, ProductUpsertDto } from '../models/models';

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

    public deleteProductCategory(productCategoryId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.productCategoriesUrl}/${productCategoryId}`);
    }

    public createProductCategory(productCategory: ProductCategoryWithThumbnail): Observable<EntityCreatedViewModel> {
        const formData = new FormData();
        if (productCategory.thumbnailImage) {
            formData.append('thumbnailImage', productCategory.thumbnailImage, productCategory.thumbnailImage.name);
        }
        if (productCategory.description) {
            formData.append('description', productCategory.description);
        }
        formData.append('name', productCategory.name);

        return this.httpClient.postWithFile<EntityCreatedViewModel>(`${this.productCategoriesUrl}`, formData);
    }

    public updateProductCategory(productCategoryId: number, productCategory: ProductCategoryWithThumbnail): Observable<void> {
        const formData = new FormData();
        console.log(productCategory);
        if (productCategory.thumbnailImage) {
            formData.append('thumbnailImage', productCategory.thumbnailImage, productCategory.thumbnailImage.name);
        }
        if (productCategory.description) {
            formData.append('description', productCategory.description);
        }
        formData.append('name', productCategory.name);

        return this.httpClient.putWithFile<void>(`${this.productCategoriesUrl}/${productCategoryId}`, formData);
    }


    public createProduct(productCategoryId: number, product: ProductUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productCategoriesUrl}/${productCategoryId}`, product);
    }
}
