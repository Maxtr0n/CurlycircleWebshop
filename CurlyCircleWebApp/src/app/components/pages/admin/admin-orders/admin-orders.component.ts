import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, debounceTime, distinctUntilChanged, tap } from 'rxjs';
import { OrdersViewModel, OrderViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';
import { OrdersDataSource } from '../OrdersDataSource';

@Component({
    selector: 'app-admin-orders',
    templateUrl: './admin-orders.component.html',
    styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent implements OnInit, AfterViewInit {
    displayedColumns: string[] = ['date', 'id', 'email', 'total'];
    dataSource: OrdersDataSource;
    resultsLength: number = 0;

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    //@ViewChild(MatSort) sort!: MatSort;

    searchWord: string = '';
    searchWordSubject: Subject<string> = new Subject<string>();

    constructor(
        private readonly authService: AuthService,
        private readonly orderService: OrderService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly route: ActivatedRoute,
    ) {
        this.dataSource = new OrdersDataSource(this.orderService);
        this.dataSource.resultsLength$.subscribe(resultsLength => this.resultsLength = resultsLength);

        this.searchWordSubject.pipe(
            debounceTime(500),
            distinctUntilChanged()
        ).subscribe({
            next: (value: string) => {
                this.searchWord = value;
                console.log(value);
            }
        });
    }

    ngOnInit(): void {
        this.dataSource.loadOrders();
    }

    ngAfterViewInit(): void {
        this.paginator.page.pipe(
            tap(() => this.loadOrdersPage())
        ).subscribe();
    }

    orderClicked(order: OrderViewModel): void {
        this.router.navigate([order.id], { relativeTo: this.route });
    }

    searchWordChanged(value: any): void {
        this.searchWordSubject.next(value);
    }

    loadOrdersPage(): void {
        this.dataSource.loadOrders(
            null,
            'desc',
            this.paginator.pageIndex,
            this.paginator.pageSize,
            null,
            null
        );
    }

}
