import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { OrderQueryParameters } from "../models/models";

@Injectable({
    providedIn: 'root'
})
export class AppHttpClient {

    private apiUrl = 'http://localhost:5000';

    public constructor(private readonly http: HttpClient) {
    }

    public get<T>(endPoint: string): Observable<T> {
        return this.http.get<T>(`${this.apiUrl}/${endPoint}`);
    }

    public getOrderWithParams<T>(endPoint: string, orderQueryParameters: OrderQueryParameters): Observable<T> {
        const httpParams = new HttpParams()
            .set('pageIndex', orderQueryParameters.pageIndex.toString())
            .set('pageSize', orderQueryParameters.pageSize.toString())
            .set('sortDirection', orderQueryParameters.sortDirection);

        if (orderQueryParameters.orderId) {
            httpParams.set('orderId', orderQueryParameters.orderId.toString());
        }

        if (orderQueryParameters.minOrderDate) {
            httpParams.set('minOrderDate', orderQueryParameters.minOrderDate.toString());
        }

        if (orderQueryParameters.maxOrderDate) {
            httpParams.set('maxOrderDate', orderQueryParameters.maxOrderDate.toString());
        }

        return this.http.get<T>(`${this.apiUrl}/${endPoint}`, {
            params: httpParams
        });
    }

    public post<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public put<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.put<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public delete<T>(endPoint: string): Observable<T> {
        return this.http.delete<T>(`${this.apiUrl}/${endPoint}`);
    }
}