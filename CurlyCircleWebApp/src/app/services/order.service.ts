import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { OrderUpsertDto } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private readonly ordersUrl = 'api/orders';

    private currentOrderSubject: BehaviorSubject<OrderUpsertDto | null>;
    public currentOrder$: Observable<OrderUpsertDto | null>;

    constructor(private readonly httpClient: AppHttpClient) {
        this.currentOrderSubject = new BehaviorSubject<OrderUpsertDto | null>(null);
        this.currentOrder$ = this.currentOrderSubject.asObservable();
    }

    public get currentOrderValue(): OrderUpsertDto | null {
        return this.currentOrderSubject.value;
    }

    public setCurrentOrder(order: OrderUpsertDto): void {
        this.currentOrderSubject.next(order);
    }

    public placeOrder(order: OrderUpsertDto): Observable<OrderUpsertDto> {
        return this.httpClient.post<OrderUpsertDto>(this.ordersUrl, order);
    }

}
