import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { ColorsViewModel, ColorUpsertDto, ColorViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ColorService {
    private readonly colorsUrl = 'api/color';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getColor(colorId: number): Observable<ColorViewModel> {
        return this.httpClient.get<ColorViewModel>(`${this.colorsUrl}/${colorId}`);
    }

    public createColor(color: ColorUpsertDto): Observable<ColorViewModel> {
        return this.httpClient.post<ColorViewModel>(`${this.colorsUrl}`, color);
    }

    public updateColor(colorId: number, color: ColorUpsertDto): Observable<void> {
        return this.httpClient.put<void>(`${this.colorsUrl}/${colorId}`, color);
    }

    public deleteColor(colorId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.colorsUrl}/${colorId}`);
    }

    public getColors(): Observable<ColorsViewModel> {
        return this.httpClient.get<ColorsViewModel>(`${this.colorsUrl}`);
    }
}
