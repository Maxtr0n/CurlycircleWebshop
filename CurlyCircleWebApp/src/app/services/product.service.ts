import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { ProductViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private readonly productsUrl = 'api/product';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getProduct(productId: number): Observable<ProductViewModel> {
        return this.httpClient.get<ProductViewModel>(`${this.productsUrl}/${productId}`);
    }

    public deleteProduct(productId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.productsUrl}/${productId}`);
    }
}
