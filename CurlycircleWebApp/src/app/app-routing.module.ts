import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { InformationComponent } from './components/information/information.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PrivacyPolicyComponent } from './components/policies/privacy-policy/privacy-policy.component';
import { RefundPolicyComponent } from './components/policies/refund-policy/refund-policy.component';
import { ShippingPolicyComponent } from './components/policies/shipping-policy/shipping-policy.component';
import { TermsOfServiceComponent } from './components/policies/terms-of-service/terms-of-service.component';
import { ProductsComponent } from './components/products/products.component';
import { SocialsComponent } from './components/socials/socials.component';
import { UserDataFormComponent } from './components/user-data-form/user-data-form.component';

const routes: Routes = [
  { path: 'fooldal', component: HomeComponent },
  { path: 'termekek', component: ProductsComponent },
  { path: 'kapcsolat', component: SocialsComponent },
  { path: 'informaciok', component: InformationComponent },
  { path: 'adatok', component: UserDataFormComponent },
  { path: 'szolgaltatasi-feltetelek', component: TermsOfServiceComponent },
  { path: 'adatkezeles', component: PrivacyPolicyComponent },
  { path: 'szallitas', component: ShippingPolicyComponent },
  { path: 'visszakuldes', component: RefundPolicyComponent },
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
