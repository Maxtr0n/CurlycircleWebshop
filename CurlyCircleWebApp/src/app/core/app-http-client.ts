import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AppHttpClient {

    public apiUrl = environment.baseUrl;

    public constructor(private readonly http: HttpClient) {
    }

    public get<T>(endPoint: string): Observable<T> {
        return this.http.get<T>(`${this.apiUrl}/${endPoint}`);
    }

    public getWithParams<T>(endPoint: string, httpParams: HttpParams): Observable<T> {
        return this.http.get<T>(`${this.apiUrl}/${endPoint}`, {
            params: httpParams
        });
    }

    public post<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public postWithFile<T>(endPoint: string, formData: FormData): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endPoint}`, formData);
    }

    public putWithFile<T>(endPoint: string, formData: FormData): Observable<T> {
        return this.http.put<T>(`${this.apiUrl}/${endPoint}`, formData);
    }

    public put<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.put<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public delete<T>(endPoint: string): Observable<T> {
        return this.http.delete<T>(`${this.apiUrl}/${endPoint}`);
    }
}