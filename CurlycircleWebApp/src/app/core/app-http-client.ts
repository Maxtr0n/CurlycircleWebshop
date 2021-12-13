import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

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

    public post<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public postFile<T>(endPoint: string, formData: FormData): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endPoint}`, formData);
    }
    public put<T>(endPoint: string, dto: Object): Observable<T> {
        return this.http.put<T>(`${this.apiUrl}/${endPoint}`, dto);
    }

    public delete<T>(endPoint: string): Observable<T> {
        return this.http.delete<T>(`${this.apiUrl}/${endPoint}`);
    }
}