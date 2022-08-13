import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { MaterialsViewModel, MaterialUpsertDto, MaterialViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class MaterialService {
    private readonly materialsUrl = 'api/material';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getMaterial(materialId: number): Observable<MaterialViewModel> {
        return this.httpClient.get<MaterialViewModel>(`${this.materialsUrl}/${materialId}`);
    }

    public createMaterial(material: MaterialUpsertDto): Observable<MaterialViewModel> {
        return this.httpClient.post<MaterialViewModel>(`${this.materialsUrl}`, material);
    }

    public updateMaterial(materialId: number, material: MaterialUpsertDto): Observable<void> {
        return this.httpClient.put<void>(`${this.materialsUrl}/${materialId}`, material);
    }

    public deleteMaterial(materialId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.materialsUrl}/${materialId}`);
    }

    public getMaterials(): Observable<MaterialsViewModel> {
        return this.httpClient.get<MaterialsViewModel>(`${this.materialsUrl}`);
    }
}
