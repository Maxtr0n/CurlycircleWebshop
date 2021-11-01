import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductsComponent } from './components/products/products.component';
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
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
        RefundPolicyComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FontAwesomeModule,
        BrowserAnimationsModule,
        MatExpansionModule,
        MatStepperModule,
        MatFormFieldModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
