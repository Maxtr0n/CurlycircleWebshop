import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { EntityCreatedViewModel, OrderQueryParameters, OrdersViewModel, OrderUpsertDto, OrderViewModel, PagedOrdersViewModel, WebPaymentRequestViewModel, WebPaymentResultViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private readonly ordersUrl = 'api/order';

    private currentOrderSubject: BehaviorSubject<OrderUpsertDto | null>;
    public currentOrder$: Observable<OrderUpsertDto | null>;

    constructor(
        private readonly httpClient: AppHttpClient
    ) {
        this.currentOrderSubject = new BehaviorSubject<OrderUpsertDto | null>(null);
        this.currentOrder$ = this.currentOrderSubject.asObservable();
    }

    public get currentOrderValue(): OrderUpsertDto | null {
        return this.currentOrderSubject.value;
    }

    public setCurrentOrder(order: OrderUpsertDto): void {
        this.currentOrderSubject.next(order);
    }

    public placeOrder(order: OrderUpsertDto): Observable<EntityCreatedViewModel> {
        return this.httpClient.post<EntityCreatedViewModel>(this.ordersUrl, order);
    }

    public placeWebPaymentOrder(order: OrderUpsertDto): Observable<WebPaymentRequestViewModel> {
        return this.httpClient.post<WebPaymentRequestViewModel>(`${this.ordersUrl}/webpayment`, order);
    }

    public getWebPaymentResult(paymentId: string): Observable<WebPaymentResultViewModel> {
        return this.httpClient.get<WebPaymentResultViewModel>(`${this.ordersUrl}/webpayment/${paymentId}`);
    }

    public getOrderPage(orderQueryParameters: OrderQueryParameters): Observable<PagedOrdersViewModel> {
        let httpParams = new HttpParams()
            .set('pageIndex', orderQueryParameters.pageIndex.toString())
            .set('pageSize', orderQueryParameters.pageSize.toString())
            .set('sortDirection', orderQueryParameters.sortDirection);

        if (orderQueryParameters.orderId !== null) {
            httpParams = httpParams.set('orderId', orderQueryParameters.orderId);
        }

        if (orderQueryParameters.minOrderDate) {
            httpParams = httpParams.set('minOrderDate', orderQueryParameters.minOrderDate.toString());
        }

        if (orderQueryParameters.maxOrderDate) {
            httpParams = httpParams.set('maxOrderDate', orderQueryParameters.maxOrderDate.toString());
        }

        return this.httpClient.getWithParams<PagedOrdersViewModel>(this.ordersUrl, httpParams);
    }

    public getOrder(id: number): Observable<OrderViewModel> {
        return this.httpClient.get<OrderViewModel>(`${this.ordersUrl}/${id}`);
    }

}
