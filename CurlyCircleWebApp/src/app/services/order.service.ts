import { Injectable } from '@angular/core';
import { AppHttpClient } from '../core/app-http-client';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private readonly ordersUrl = 'api/orders';

    constructor(private readonly httpClient: AppHttpClient) { }
}
