import { NgModule } from '@angular/core';
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
import { ProductCategoriesComponent } from './components/pages/product-categories/product-categories.component';
import { ProductsComponent } from './components/pages/products/products.component';
import { ProductDetailsComponent } from './components/pages/product-details/product-details.component';
import { ShopComponent } from './components/pages/shop/shop.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

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
    ],
    providers: [
        httpInterceptorProviders,
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
