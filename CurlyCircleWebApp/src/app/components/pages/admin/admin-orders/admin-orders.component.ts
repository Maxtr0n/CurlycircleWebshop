import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, debounceTime, distinctUntilChanged, tap, merge } from 'rxjs';
import { OrdersViewModel, OrderViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';
import { OrdersDataSource } from '../../../../models/OrdersDataSource';

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
    @ViewChild(MatSort) sort!: MatSort;

    searchWord: string = '';
    range = new FormGroup({
        start: new FormControl<Date | null>(null),
        end: new FormControl<Date | null>(null),
    });

    constructor(
        private readonly authService: AuthService,
        private readonly orderService: OrderService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly route: ActivatedRoute,
    ) {
        this.dataSource = new OrdersDataSource(this.orderService);
        this.dataSource.resultsLength$.subscribe(resultsLength => this.resultsLength = resultsLength);
    }

    ngOnInit(): void {
        this.dataSource.loadOrders();
    }

    ngAfterViewInit(): void {
        //reset paginator after sorting
        this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

        // on sort or paginate events, load a new page
        merge(this.sort.sortChange, this.paginator.page).pipe(
            tap(() => this.loadOrdersPage())
        ).subscribe();
    }

    orderClicked(order: OrderViewModel): void {
        this.router.navigate([order.id], { relativeTo: this.route });
    }

    filter(): void {
        this.paginator.pageIndex = 0;
        this.loadOrdersPage();
    }

    clearFilters() {
        this.searchWord = '';
        this.range.reset();
        this.loadOrdersPage();
    }

    loadOrdersPage(): void {
        this.dataSource.loadOrders(
            this.searchWord,
            this.sort.direction,
            this.paginator.pageIndex,
            this.paginator.pageSize,
            this.range.value.start,
            this.range.value.end
        );
    }
}
