import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, ProductsViewModel, ProductUpsertDto, ProductViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private readonly productsApiUrl = 'api/products';

    constructor(private readonly httpClient: AppHttpClient) { }

    getProducts(): Observable<ProductsViewModel> {
        return this.httpClient.get<ProductsViewModel>(`${this.productsApiUrl}`);
    }

    getProduct(productId: number): Observable<ProductViewModel> {
        return this.httpClient.get<ProductViewModel>(`${this.productsApiUrl}/${productId}`);
    }

    updateProduct(productId: number, dto: ProductUpsertDto): Observable<void> {
        return this.httpClient.put(`${this.productsApiUrl}/${productId}`, dto);
    }

    addProduct(dto: ProductUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.productsApiUrl}`, dto);
    }

    deleteProduct(productId: number): Observable<void> {
        return this.httpClient.delete(`${this.productsApiUrl}/${productId}`);
    }
}
