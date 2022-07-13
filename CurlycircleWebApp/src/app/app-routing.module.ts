import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './components/pages/cart/cart.component';
import { ContactComponent } from './components/pages/contact/contact.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { MyOrdersComponent } from './components/pages/my-orders/my-orders.component';
import { ProductCategoriesComponent } from './components/pages/shop/product-categories/product-categories.component';
import { ProductDetailsComponent } from './components/pages/shop/product-details/product-details.component';
import { ProductsComponent } from './components/pages/shop/products/products.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { RegistrationComponent } from './components/pages/registration/registration.component';
import { ShopComponent } from './components/pages/shop/shop.component';
import { UserGuard } from './guards/user.guard';
import { OrderComponent } from './components/pages/order/order.component';
import { OrderOptionsComponent } from './components/pages/order-options/order-options.component';
import { ConfirmOrderComponent } from './components/pages/confirm-order/confirm-order.component';
import { OrderSuccessComponent } from './components/pages/order-success/order-success.component';
import { AdminOrdersComponent } from './components/pages/admin/admin-orders/admin-orders.component';
import { AdminGuard } from './guards/admin.guard';
import { AdminOrderDetailsComponent } from './components/pages/admin/admin-order-details/admin-order-details.component';
import { AdminProductsComponent } from './components/pages/admin/admin-products/admin-products.component';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: 'profile', component: ProfileComponent, canActivate: [UserGuard] },
    { path: 'my-orders', component: MyOrdersComponent, canActivate: [UserGuard] },
    {
        path: 'shop', component: ShopComponent, children: [
            { path: '', pathMatch: 'full', data: { breadcrumb: { alias: 'ProductCategories' } }, component: ProductCategoriesComponent },
            { path: ':productCategoryId', redirectTo: ':productCategoryId/products', pathMatch: 'full' },
            {
                path: ':productCategoryId/products', children: [
                    { path: '', pathMatch: 'full', data: { breadcrumb: { alias: 'Products' } }, component: ProductsComponent },
                    { path: ':productId', data: { breadcrumb: { alias: 'ProductDetails' } }, component: ProductDetailsComponent }
                ]
            },

        ]
    },
    { path: 'cart', component: CartComponent },
    { path: 'order', component: OrderComponent },
    { path: 'order-options', component: OrderOptionsComponent },
    { path: 'confirm-order', component: ConfirmOrderComponent },
    { path: 'order-success', component: OrderSuccessComponent },
    { path: 'contact', component: ContactComponent },
    {
        path: 'admin-orders', canActivate: [AdminGuard], children: [
            { path: '', pathMatch: 'full', component: AdminOrdersComponent },
            { path: ':orderId', component: AdminOrderDetailsComponent }
        ]
    },
    { path: 'admin-products', component: AdminProductsComponent, canActivate: [AdminGuard] },
    { path: '', component: HomeComponent },
    { path: '**', component: HomeComponent }, //TODO: PageNotFoundComponent
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
