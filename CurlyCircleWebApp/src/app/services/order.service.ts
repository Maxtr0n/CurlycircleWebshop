import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { OrdersViewModel, OrderUpsertDto, OrderViewModel, PagedOrdersViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private readonly ordersUrl = 'api/order';

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
        console.log('rendelés leadva', order);
        return this.httpClient.post<OrderUpsertDto>(this.ordersUrl, order);
    }

    public getOrderPage(): Observable<PagedOrdersViewModel> {
        return this.httpClient.get<PagedOrdersViewModel>(this.ordersUrl);
    }

    public getOrder(id: number): Observable<OrderViewModel> {
        return this.httpClient.get<OrderViewModel>(`${this.ordersUrl}/${id}`);
    }

}
