import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { format } from "date-fns";
import { BehaviorSubject, catchError, Observable, of, finalize } from "rxjs";
import { OrderQueryParameters, OrderViewModel } from "src/app/models/models";
import { OrderService } from "src/app/services/order.service";

export class OrdersDataSource implements DataSource<OrderViewModel> {

    private ordersSubject = new BehaviorSubject<OrderViewModel[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private resultsLengthSubject = new BehaviorSubject<number>(0);

    public resultsLength$ = this.resultsLengthSubject.asObservable();
    public loading$ = this.loadingSubject.asObservable();

    constructor(private orderService: OrderService) { }

    connect(collectionViewer: CollectionViewer): Observable<readonly OrderViewModel[]> {
        return this.ordersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.ordersSubject.complete();
        this.loadingSubject.complete();
    }

    loadOrders(orderId: string | null = null, sortDirection = 'desc', pageIndex = 0, pageSize = 25,
        minOrderDate: Date | null = null, maxOrderDate: Date | null = null): void {
        this.loadingSubject.next(true);

        let minDate: string | null = null;
        let maxDate: string | null = null;

        if (minOrderDate) {
            minDate = format(minOrderDate, 'yyyy-MM-dd');
        }

        if (maxOrderDate) {
            maxDate = format(maxOrderDate, 'yyyy-MM-dd');
        }

        const orderQueryParameters: OrderQueryParameters = {
            pageIndex,
            pageSize,
            orderId,
            sortDirection,
            minOrderDate: minDate,
            maxOrderDate: maxDate
        };

        this.orderService.getOrderPage(orderQueryParameters).subscribe({
            next: (pagedOrdersViewModel) => {
                this.loadingSubject.next(false);
                this.resultsLengthSubject.next(pagedOrdersViewModel.totalCount);
                this.ordersSubject.next(pagedOrdersViewModel.orders);
            },
            error: () => {
                this.ordersSubject.next([]);
                this.loadingSubject.next(false);
                this.resultsLengthSubject.next(0);
            }
        });
    }
}