import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { MaterialViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class MaterialService {
    private readonly materialsUrl = 'api/material';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getMaterial(materialId: number): Observable<MaterialViewModel> {
        return this.httpClient.get<MaterialViewModel>(`${this.materialsUrl}/${materialId}`);
    }

    public createMaterial(material: MaterialViewModel): Observable<MaterialViewModel> {
        return this.httpClient.post<MaterialViewModel>(`${this.materialsUrl}`, material);
    }

    public updateMaterial(materialId: number, material: MaterialViewModel): Observable<void> {
        return this.httpClient.put<void>(`${this.materialsUrl}/${materialId}`, material);
    }

    public deleteMaterial(materialId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.materialsUrl}/${materialId}`);
    }

    public getMaterials(): Observable<MaterialViewModel[]> {
        return this.httpClient.get<MaterialViewModel[]>(`${this.materialsUrl}`);
    }
}
