import { Injectable } from '@angular/core';
import { AppHttpClient } from '../core/app-http-client';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private readonly productsUrl = 'api/products';

    constructor(private readonly httpClient: AppHttpClient) { }
}
