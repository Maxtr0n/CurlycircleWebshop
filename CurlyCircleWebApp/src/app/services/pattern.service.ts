import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppHttpClient } from '../core/app-http-client';
import { PatternsViewModel, PatternUpsertDto, PatternViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class PatternService {
    private readonly patternsUrl = 'api/pattern';

    constructor(private readonly httpClient: AppHttpClient) { }

    public getPattern(patternId: number): Observable<PatternViewModel> {
        return this.httpClient.get<PatternViewModel>(`${this.patternsUrl}/${patternId}`);
    }

    public createPattern(pattern: PatternUpsertDto): Observable<PatternViewModel> {
        return this.httpClient.post<PatternViewModel>(`${this.patternsUrl}`, pattern);
    }

    public updatePattern(patternId: number, pattern: PatternUpsertDto): Observable<void> {
        return this.httpClient.put<void>(`${this.patternsUrl}/${patternId}`, pattern);
    }

    public deletePattern(patternId: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.patternsUrl}/${patternId}`);
    }

    public getPatterns(): Observable<PatternsViewModel> {
        return this.httpClient.get<PatternsViewModel>(`${this.patternsUrl}`);
    }
}
