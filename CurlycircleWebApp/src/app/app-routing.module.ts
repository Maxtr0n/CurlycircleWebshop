import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './components/pages/cart/cart.component';
import { ContactComponent } from './components/pages/contact/contact.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { MyOrdersComponent } from './components/pages/my-orders/my-orders.component';
import { ProductCategoriesComponent } from './components/pages/product-categories/product-categories.component';
import { ProductDetailsComponent } from './components/pages/product-details/product-details.component';
import { ProductsComponent } from './components/pages/products/products.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { RegistrationComponent } from './components/pages/registration/registration.component';
import { ShopComponent } from './components/pages/shop/shop.component';
import { UserGuard } from './guards/user.guard';

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
    { path: 'contact', component: ContactComponent },
    { path: '', component: HomeComponent },
    { path: '**', component: HomeComponent }, //TODO: PageNotFoundComponent
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
