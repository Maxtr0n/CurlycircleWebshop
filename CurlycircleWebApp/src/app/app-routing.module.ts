import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './components/admin/admin.component';
import { CartComponent } from './components/cart/cart.component';
import { HomeComponent } from './components/home/home.component';
import { InformationComponent } from './components/information/information.component';
import { OrdersComponent } from './components/orders/orders.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PrivacyPolicyComponent } from './components/policies/privacy-policy/privacy-policy.component';
import { RefundPolicyComponent } from './components/policies/refund-policy/refund-policy.component';
import { ShippingPolicyComponent } from './components/policies/shipping-policy/shipping-policy.component';
import { TermsOfServiceComponent } from './components/policies/terms-of-service/terms-of-service.component';
import { ProductCategoriesComponent } from './components/product-categories/product-categories.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ProductsComponent } from './components/products/products.component';
import { SocialsComponent } from './components/socials/socials.component';
import { UserDataFormComponent } from './components/user-data-form/user-data-form.component';

const routes: Routes = [
    { path: 'fooldal', component: HomeComponent },
    { path: 'kapcsolat', component: SocialsComponent },
    { path: 'informaciok', component: InformationComponent },
    { path: 'order', component: UserDataFormComponent },
    { path: 'szolgaltatasi-feltetelek', component: TermsOfServiceComponent },
    { path: 'adatkezeles', component: PrivacyPolicyComponent },
    { path: 'szallitas', component: ShippingPolicyComponent },
    { path: 'visszakuldes', component: RefundPolicyComponent },
    {
        path: 'productCategories', children: [
            {
                path: '',
                pathMatch: 'full',
                component: ProductCategoriesComponent
            },
            {
                path: ':id',
                component: ProductsComponent,
            }
        ]
    },
    {
        path: 'products', children: [
            {
                path: ':id',
                component: ProductDetailsComponent,
            }
        ]
    },
    { path: 'admin', component: AdminComponent },
    { path: 'orders', component: OrdersComponent },
    { path: 'shopping-cart', component: CartComponent },
    { path: '', component: HomeComponent },
    { path: '**', component: PageNotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
