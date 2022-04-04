import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, OrderItemsViewModel, OrderItemUpsertDto, OrderItemViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class OrderItemService {
    private readonly orderitemsApiUrl = 'api/orderitems';

    constructor(private readonly httpClient: AppHttpClient) { }

    getOrderitem(orderitemId: number): Observable<OrderItemViewModel> {
        return this.httpClient.get<OrderItemViewModel>(`${this.orderitemsApiUrl}/${orderitemId}`);
    }

    updateOrderitem(orderitemId: number, dto: OrderItemUpsertDto): Observable<void> {
        return this.httpClient.put(`${this.orderitemsApiUrl}/${orderitemId}`, dto);
    }

    addOrderitem(dto: OrderItemUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(`${this.orderitemsApiUrl}`, dto);
    }

    deleteOrderitem(orderitemId: number): Observable<void> {
        return this.httpClient.delete(`${this.orderitemsApiUrl}/${orderitemId}`);
    }
}
