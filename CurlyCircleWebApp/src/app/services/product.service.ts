import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, ProductViewModel, ProductWithImages } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private readonly productsUrl = 'api/product';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getProduct(productId: number): Observable<ProductViewModel> {
        return this.httpClient.get<ProductViewModel>(`${this.productsUrl}/${productId}`);
    }

    public createProduct(product: ProductWithImages): Observable<EntityCreatedViewModel> {
        const formData = new FormData();
        if (product.thumbnailImage) {
            formData.append('thumbnailImage', product.thumbnailImage, product.thumbnailImage.name);
        }

        for (let i = 0; i < product.productImages.length; i++) {
            formData.append('productImage' + i, product.productImages[i], product.productImages[i].name);
        }

        if (product.description) {
            formData.append('description', product.description);
        }
        formData.append('name', product.name);

        return this.httpClient.postWithFile<EntityCreatedViewModel>(`${this.productsUrl}`, formData);
    }

    public updateProduct(productId: number, product: ProductWithImages): Observable<void> {
        const formData = new FormData();
        console.log(product);
        if (product.thumbnailImage) {
            formData.append('thumbnailImage', product.thumbnailImage, product.thumbnailImage.name);
        }
        if (product.description) {
            formData.append('description', product.description);
        }
        formData.append('name', product.name);

        return this.httpClient.putWithFile<void>(`${this.productsUrl}/${productId}`, formData);
    }

    public deleteProduct(productId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.productsUrl}/${productId}`);
    }
}
