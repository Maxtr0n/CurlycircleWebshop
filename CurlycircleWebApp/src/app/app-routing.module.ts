import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { InformationComponent } from './components/information/information.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProductsComponent } from './components/products/products.component';
import { SocialsComponent } from './components/socials/socials.component';

const routes: Routes = [
  { path: 'fooldal', component: HomeComponent },
  { path: 'termekek', component: ProductsComponent },
  { path: 'kapcsolat', component: SocialsComponent },
  { path: 'informaciok', component: InformationComponent },
  { path: '', component: HomeComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
