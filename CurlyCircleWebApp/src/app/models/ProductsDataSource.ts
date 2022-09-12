import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable } from "rxjs";
import { ProductService } from "../services/product.service";
import { PagedProductsViewModel, ProductQueryParameters, ProductViewModel } from "./models";

export class ProductsDataSource implements DataSource<ProductViewModel> {

    private productsSubject = new BehaviorSubject<ProductViewModel[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private resultsLengthSubject = new BehaviorSubject<number>(0);

    public resultsLength$ = this.resultsLengthSubject.asObservable();
    public loading$ = this.loadingSubject.asObservable();
    public products$ = this.productsSubject.asObservable();

    constructor(private productService: ProductService) { }

    connect(collectionViewer: CollectionViewer): Observable<readonly ProductViewModel[]> {
        return this.productsSubject.asObservable();
    }
    disconnect(collectionViewer: CollectionViewer): void {
        this.productsSubject.complete();
        this.loadingSubject.complete();
    }

    loadProducts(productCategoryId: number, pageIndex = 0,
        minPrice: number | null = null, maxPrice: number | null = null, colorIds: number[] = [],
        materialIds: number[] = [], patternIds: number[] = []): void {
        this.loadingSubject.next(true);

        const productQueryParameters: ProductQueryParameters = {
            pageIndex,
            pageSize: 6,
            productCategoryId,
            minPrice,
            maxPrice,
            colorIds,
            materialIds,
            patternIds
        };

        this.productService.getProductPage(productQueryParameters).subscribe({
            next: (pagedProductsViewModel: PagedProductsViewModel) => {
                this.loadingSubject.next(false);
                this.resultsLengthSubject.next(pagedProductsViewModel.totalCount);
                this.productsSubject.next(pagedProductsViewModel.products);
            },
            error: () => {
                this.productsSubject.next([]);
                this.loadingSubject.next(false);
                this.resultsLengthSubject.next(0);
            }
        });
    }

}