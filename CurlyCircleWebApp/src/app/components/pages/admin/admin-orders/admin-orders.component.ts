import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { OrdersViewModel, OrderViewModel } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
    selector: 'app-admin-orders',
    templateUrl: './admin-orders.component.html',
    styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent implements OnInit, AfterViewInit {
    displayedColumns: string[] = ['date', 'id', 'email', 'total'];
    dataSource = new MatTableDataSource<OrderViewModel>([]);
    @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(
        private readonly authService: AuthService,
        private readonly orderService: OrderService,
        private readonly router: Router,
        private readonly snackBar: MatSnackBar,
        private readonly route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        this.orderService.getOrders().subscribe({
            next: (orders: OrdersViewModel) => {
                this.dataSource.data = orders.orders;
            }
        });
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator;
    }

    orderClicked(order: OrderViewModel): void {
        this.router.navigate([order.id], { relativeTo: this.route });
    }

}
