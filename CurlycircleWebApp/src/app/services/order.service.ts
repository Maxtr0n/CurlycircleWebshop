import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, OrderItemsViewModel, OrderItemUpsertDto, OrderItemViewModel, OrdersViewModel, OrderUpsertDto, OrderViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private readonly ordersApiUrl = 'api/orders';

    constructor(private readonly httpClient: AppHttpClient) { }

    getOrders(): Observable<OrdersViewModel> {
        return this.httpClient.get<OrdersViewModel>(`${this.ordersApiUrl}`);
    }

    getOrder(orderId: number): Observable<OrderViewModel> {
        return this.httpClient.get<OrderViewModel>(`${this.ordersApiUrl}/${orderId}`);
    }

    getOrderOrderItems(orderId: number): Observable<OrderItemsViewModel> {
        return this.httpClient.get<OrderItemsViewModel>(`${this.ordersApiUrl}/${orderId}/orderItems`);
    }

    updateOrder(orderId: number, dto: OrderUpsertDto): Observable<void> {
        return this.httpClient.put(`${this.ordersApiUrl}/${orderId}`, dto);
    }

    addOrder(dto: OrderUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.ordersApiUrl}`, dto);
    }

    addOrderOrderItem(orderId: number, dto: OrderItemUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.ordersApiUrl}/${orderId}/orderItems`, dto);
    }

    deleteOrder(orderId: number): Observable<void> {
        return this.httpClient.delete(`${this.ordersApiUrl}/${orderId}`);
    }
}
