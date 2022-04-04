import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductsComponent } from './components/product/products/products.component';
import { SocialsComponent } from './components/socials/socials.component';
import { InformationComponent } from './components/information/information.component';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { UserDataFormComponent } from './components/user-data-form/user-data-form.component';
import { PrivacyPolicyComponent } from './components/policies/privacy-policy/privacy-policy.component';
import { TermsOfServiceComponent } from './components/policies/terms-of-service/terms-of-service.component';
import { ShippingPolicyComponent } from './components/policies/shipping-policy/shipping-policy.component';
import { RefundPolicyComponent } from './components/policies/refund-policy/refund-policy.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { MatStepperModule } from '@angular/material/stepper';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { TokenInterceptor } from './core/token.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProductDetailsComponent } from './components/product/product-details/product-details.component';
import { ProductCategoriesComponent } from './components/product/product-categories/product-categories.component';
import { ToastrModule } from 'ngx-toastr';
import { MatIconModule } from '@angular/material/icon';
import { CartComponent } from './components/cart/cart.component';
import { AdminComponent } from './components/admin/admin.component';
import { OrdersComponent } from './components/orders/orders.component';
import { OrderDetailsComponent } from './components/orders/order-details/order-details.component';
import { TableComponent } from './components/table/table.component';
import { DatePipe } from '@angular/common';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { ErrorInterceptor } from './core/error.interceptor';
import { CartItemComponent } from './components/cart/cart-item/cart-item.component';
import { ProductItemComponent } from './components/product/product-item/product-item.component';

@NgModule({
    declarations: [
        AppComponent,
        FooterComponent,
        NavBarComponent,
        ProductsComponent,
        SocialsComponent,
        InformationComponent,
        HomeComponent,
        PageNotFoundComponent,
        UserDataFormComponent,
        PrivacyPolicyComponent,
        TermsOfServiceComponent,
        ShippingPolicyComponent,
        RefundPolicyComponent,
        ProductDetailsComponent,
        ProductCategoriesComponent,
        ProductItemComponent,
        CartComponent,
        AdminComponent,
        OrdersComponent,
        OrderDetailsComponent,
        TableComponent,
        CartItemComponent
    ],
    imports: [
        BrowserModule,
        ToastrModule.forRoot({
            positionClass: 'toast-top-center',
            closeButton: true
        }),
        AppRoutingModule,
        FontAwesomeModule,
        BrowserAnimationsModule,
        MatExpansionModule,
        MatStepperModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        MatTableModule,
        HttpClientModule,
        MatIconModule,
    ],
    providers: [
        DatePipe,
        { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
