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
import { ProductCardComponent } from './components/shared/product-card/product-card.component';
import { CartComponent } from './components/pages/cart/cart.component';
import { OrderComponent } from './components/pages/order/order.component';
import { HufPipe } from './utilities/pipes/huf-pipe';
import { QuantityPickerComponent } from './components/shared/quantity-picker/quantity-picker.component';
import { OrderOptionsComponent } from './components/pages/order-options/order-options.component';
import { ConfirmOrderComponent } from './components/pages/confirm-order/confirm-order.component';
import { OrderCompleteComponent } from './components/pages/order-complete/order-complete.component';
import { AdminOrdersComponent } from './components/pages/admin/admin-orders/admin-orders.component';
import { AdminOrderDetailsComponent } from './components/pages/admin/admin-order-details/admin-order-details.component';
import { ShippingMethodPipe } from './utilities/pipes/shipping-method-pipe';
import { PaymentMethodPipe } from './utilities/pipes/payment-method-pipe';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { hu } from 'date-fns/locale';
import { DateFnsAdapter, MatDateFnsModule, MAT_DATE_FNS_FORMATS } from '@angular/material-date-fns-adapter';
import { AddProductCategoryDialogComponent } from './components/dialogs/add-product-category-dialog/add-product-category-dialog.component';
import { DeleteProductCategoryDialogComponent } from './components/dialogs/delete-product-category-dialog/delete-product-category-dialog.component';
import { ModifyProductCategoryDialogComponent } from './components/dialogs/modify-product-category-dialog/modify-product-category-dialog.component';
import { ModifyProductDialogComponent } from './components/dialogs/modify-product-dialog/modify-product-dialog.component';
import { DeleteProductDialogComponent } from './components/dialogs/delete-product-dialog/delete-product-dialog.component';
import { AddProductDialogComponent } from './components/dialogs/add-product-dialog/add-product-dialog.component';
import { NgxMatFileInputModule } from '@angular-material-components/file-input';
import { ProductCategoryCardComponent } from './components/shared/product-category-card/product-category-card.component';
import { AdminColorsPatternsMaterialsComponent } from './components/pages/admin/admin-colors-patterns-materials/admin-colors-patterns-materials.component';
import { AddColorDialogComponent } from './components/dialogs/add-color-dialog/add-color-dialog.component';
import { DeleteColorDialogComponent } from './components/dialogs/delete-color-dialog/delete-color-dialog.component';
import { DeleteMaterialDialogComponent } from './components/dialogs/delete-material-dialog/delete-material-dialog.component';
import { AddMaterialDialogComponent } from './components/dialogs/add-material-dialog/add-material-dialog.component';
import { AddPatternDialogComponent } from './components/dialogs/add-pattern-dialog/add-pattern-dialog.component';
import { DeletePatternDialogComponent } from './components/dialogs/delete-pattern-dialog/delete-pattern-dialog.component';
import { ProductFiltersComponent } from './components/pages/shop/product-filters/product-filters.component';
import { PrimengModule } from './primeng/primeng.module';

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
        OrderCompleteComponent,
        AdminOrdersComponent,
        AdminOrderDetailsComponent,
        AddProductCategoryDialogComponent,
        DeleteProductCategoryDialogComponent,
        ModifyProductCategoryDialogComponent,
        ModifyProductDialogComponent,
        DeleteProductDialogComponent,
        AddProductDialogComponent,
        ProductCategoryCardComponent,
        AdminColorsPatternsMaterialsComponent,
        AddColorDialogComponent,
        DeleteColorDialogComponent,
        DeleteMaterialDialogComponent,
        AddMaterialDialogComponent,
        AddPatternDialogComponent,
        DeletePatternDialogComponent,
        ProductFiltersComponent,
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
        FontAwesomeModule,
        NgxMatFileInputModule,
        PrimengModule
    ],
    providers: [
        httpInterceptorProviders,
        { provide: DateAdapter, useClass: DateFnsAdapter },
        { provide: MAT_DATE_LOCALE, useValue: hu },
        { provide: MAT_DATE_FORMATS, useValue: MAT_DATE_FNS_FORMATS },
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
