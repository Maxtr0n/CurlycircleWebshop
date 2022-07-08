import { DEFAULT_CURRENCY_CODE, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProviders } from './core/http-interceptor-providers';

import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LayoutComponent } from './components/layout/layout.component';
import { SidenavListComponent } from './components/sidenav-list/sidenav-list.component';
import { RegistrationComponent } from './components/pages/registration/registration.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { MyOrdersComponent } from './components/pages/my-orders/my-orders.component';
import { ContactComponent } from './components/pages/contact/contact.component';
import { ProductCategoriesComponent } from './components/pages/shop/product-categories/product-categories.component';
import { ProductsComponent } from './components/pages/shop/products/products.component';
import { ProductDetailsComponent } from './components/pages/shop/product-details/product-details.component';
import { ShopComponent } from './components/pages/shop/shop.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductCardComponent } from './components/shared/product-card/product-card.component';
import { CartComponent } from './components/pages/cart/cart.component';
import { OrderComponent } from './components/pages/order/order.component';
import { HufPipe } from './utilities/pipes/huf-pipe';
import { QuantityPickerComponent } from './components/shared/quantity-picker/quantity-picker.component';
import { OrderOptionsComponent } from './components/pages/order-options/order-options.component';
import { ConfirmOrderComponent } from './components/pages/confirm-order/confirm-order.component';
import { OrderSuccessComponent } from './components/pages/order-success/order-success.component';
import { AdminOrdersComponent } from './components/pages/admin/admin-orders/admin-orders.component';
import { AdminOrderDetailsComponent } from './components/pages/admin/admin-order-details/admin-order-details.component';
import { ShippingMethodPipe } from './utilities/pipes/shipping-method-pipe';
import { PaymentMethodPipe } from './utilities/pipes/payment-method-pipe';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { hu } from 'date-fns/locale';
import { MatDateFnsModule } from '@angular/material-date-fns-adapter';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        FooterComponent,
        HeaderComponent,
        LayoutComponent,
        SidenavListComponent,
        RegistrationComponent,
        ProfileComponent,
        MyOrdersComponent,
        ContactComponent,
        ProductCategoriesComponent,
        ProductsComponent,
        ProductDetailsComponent,
        ShopComponent,
        ProductCardComponent,
        CartComponent,
        OrderComponent,
        HufPipe,
        ShippingMethodPipe,
        PaymentMethodPipe,
        QuantityPickerComponent,
        OrderOptionsComponent,
        ConfirmOrderComponent,
        OrderSuccessComponent,
        AdminOrdersComponent,
        AdminOrderDetailsComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        HttpClientModule,
        MaterialModule,
        FlexLayoutModule,
        ReactiveFormsModule,
        FormsModule,
        BreadcrumbModule,
        NgbModule,
        FontAwesomeModule,
    ],
    providers: [
        httpInterceptorProviders,
        { provide: MAT_DATE_LOCALE, useValue: hu },
        { provide: MAT_DATE_FORMATS, useValue: hu },
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
