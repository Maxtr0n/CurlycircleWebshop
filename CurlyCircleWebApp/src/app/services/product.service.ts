import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, PagedProductsViewModel, ProductQueryParameters, ProductViewModel, ProductWithImages } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private readonly productsUrl = 'api/product';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getProduct(productId: number): Observable<ProductViewModel> {
        return this.httpClient.get<ProductViewModel>(`${this.productsUrl}/${productId}`);
    }

    public getProductPage(productQueryParameters: ProductQueryParameters): Observable<PagedProductsViewModel> {
        let httpParams = new HttpParams()
            .set('pageIndex', productQueryParameters.pageIndex.toString())
            .set('pageSize', productQueryParameters.pageSize.toString());

        if (productQueryParameters.productCategoryId !== null) {
            httpParams = httpParams.set('productCategoryId', productQueryParameters.productCategoryId);
        }

        if (productQueryParameters.minPrice !== null) {
            httpParams = httpParams.set('minPrice', productQueryParameters.minPrice);
        }

        if (productQueryParameters.maxPrice !== null) {
            httpParams = httpParams.set('maxPrice', productQueryParameters.maxPrice);
        }

        if (productQueryParameters.colorIds !== null && productQueryParameters.colorIds.length > 0) {
            for (let colorId of productQueryParameters.colorIds) {
                httpParams = httpParams.set('colorIds', colorId);
            }
        }

        if (productQueryParameters.materialIds !== null && productQueryParameters.materialIds.length > 0) {
            for (let materialId of productQueryParameters.materialIds) {
                httpParams = httpParams.set('materialIds', materialId);
            }
        }

        if (productQueryParameters.patternIds !== null && productQueryParameters.patternIds.length > 0) {
            for (let patternId of productQueryParameters.patternIds) {
                httpParams = httpParams.set('patternIds', patternId);
            }
        }

        //TO DELETE
        console.log("getProducts called");
        console.log(httpParams);

        return this.httpClient.getWithParams<PagedProductsViewModel>(this.productsUrl, httpParams);
    }

    public createProduct(product: ProductWithImages): Observable<EntityCreatedViewModel> {
        const formData = new FormData();

        formData.append('name', product.name);
        formData.append('price', product.price.toString());
        formData.append('productCategoryId', product.productCategoryId.toString());

        if (product.thumbnailImage) {
            formData.append('thumbnailImage', product.thumbnailImage, product.thumbnailImage.name);
        }

        for (let i = 0; i < product.productImages.length; i++) {
            formData.append('productImage' + i, product.productImages[i], product.productImages[i].name);
        }

        if (product.description) {
            formData.append('description', product.description);
        }

        if (product.isAvailable !== null) {
            formData.append('isAvailable', product.isAvailable.toString());
        }

        let colors = '';
        for (let i = 0; i < product.colorIds.length; i++) {
            colors = colors.concat(product.colorIds[i].toString() + ';');
        }

        formData.append('colorIds', colors);

        if (product.patternId) {
            formData.append('patternId', product.patternId.toString());
        }

        if (product.materialId) {
            formData.append('materialId', product.materialId.toString());
        }

        return this.httpClient.postWithFile<EntityCreatedViewModel>(`${this.productsUrl}`, formData);
    }

    public updateProduct(productId: number, product: ProductWithImages): Observable<void> {
        const formData = new FormData();

        formData.append('name', product.name);
        formData.append('price', product.price.toString());
        formData.append('productCategoryId', product.productCategoryId.toString());

        if (product.thumbnailImage) {
            formData.append('thumbnailImage', product.thumbnailImage, product.thumbnailImage.name);
        }

        for (let i = 0; i < product.productImages.length; i++) {
            formData.append('productImage' + i, product.productImages[i], product.productImages[i].name);
        }

        if (product.description) {
            formData.append('description', product.description);
        }

        if (product.isAvailable !== null) {
            formData.append('isAvailable', product.isAvailable.toString());
        }

        let colors = '';
        for (let i = 0; i < product.colorIds.length; i++) {
            colors = colors.concat(product.colorIds[i].toString() + ';');
        }

        formData.append('colorIds', colors);

        if (product.patternId) {
            formData.append('patternId', product.patternId.toString());
        }

        if (product.materialId) {
            formData.append('materialId', product.materialId.toString());
        }

        return this.httpClient.putWithFile<void>(`${this.productsUrl}/${productId}`, formData);
    }

    public deleteProduct(productId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.productsUrl}/${productId}`);
    }
}
