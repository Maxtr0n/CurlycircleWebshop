import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { OrdersViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private readonly userUrl = 'api/user';

    constructor(
        private readonly httpClient: AppHttpClient
    ) { }

    public getUserOrders(userId: number): Observable<OrdersViewModel> {
        return this.httpClient.get<OrdersViewModel>(`${this.userUrl}/${userId}/orders`);
    }
}
