import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ColorViewModel, MaterialViewModel, PatternViewModel } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class FilterService {

    private selectedColorsSubject: BehaviorSubject<number[]>;
    public selectedColors$: Observable<number[]>;

    private selectedMaterialsSubject: BehaviorSubject<number[]>;
    public selectedMaterials$: Observable<number[]>;

    private selectedPatternsSubject: BehaviorSubject<number[]>;
    public selectedPatterns$: Observable<number[]>;

    private selectedPricesSubject: BehaviorSubject<number[]>;
    public selectedPrices$: Observable<number[]>;

    constructor() {
        this.selectedColorsSubject = new BehaviorSubject<number[]>([]);
        this.selectedColors$ = this.selectedColorsSubject.asObservable();

        this.selectedMaterialsSubject = new BehaviorSubject<number[]>([]);
        this.selectedMaterials$ = this.selectedMaterialsSubject.asObservable();

        this.selectedPatternsSubject = new BehaviorSubject<number[]>([]);
        this.selectedPatterns$ = this.selectedPatternsSubject.asObservable();

        this.selectedPricesSubject = new BehaviorSubject<number[]>([]);
        this.selectedPrices$ = this.selectedPricesSubject.asObservable();
    }

    public get selectedColorsValue(): number[] {
        return this.selectedColorsSubject.value;
    }

    public get selectedMaterialsValue(): number[] {
        return this.selectedMaterialsSubject.value;
    }

    public get selectedPatternsValue(): number[] {
        return this.selectedPatternsSubject.value;
    }

    public get selectedPricesValue(): number[] {
        return this.selectedPricesSubject.value;
    }

    public updateSelectedColors(colors: number[]): void {
        this.selectedColorsSubject.next(colors);
    }

    public updateSelectedMaterials(materials: number[]): void {
        this.selectedMaterialsSubject.next(materials);
    }

    public updateSelectedPatterns(patterns: number[]): void {
        this.selectedPatternsSubject.next(patterns);
    }

    public updateSelectedPrices(prices: number[]): void {
        this.selectedPricesSubject.next(prices);
    }
}
