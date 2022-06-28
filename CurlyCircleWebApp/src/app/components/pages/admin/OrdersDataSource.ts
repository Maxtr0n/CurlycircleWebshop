import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable } from "rxjs";
import { OrderViewModel } from "src/app/models/models";
import { OrderService } from "src/app/services/order.service";

export class OrdersDataSource implements DataSource<OrderViewModel> {

    private ordersSubject = new BehaviorSubject<OrderViewModel[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private orderService: OrderService) { }

    connect(collectionViewer: CollectionViewer): Observable<readonly OrderViewModel[]> {
        return this.ordersSubject.asObservable();
    }
    disconnect(collectionViewer: CollectionViewer): void {
        this.ordersSubject.complete();
        this.loadingSubject.complete();
    }

    loadOrders(filter = '', sortDirection = 'desc', pageIndex = 0, pageSize = 10) {
        this.loadingSubject.next(true);

        // TODO

        /* this.coursesService.findLessons(courseId, filter, sortDirection,
            pageIndex, pageSize).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        )
        .subscribe(lessons => this.lessonsSubject.next(lessons)); */
    }
}