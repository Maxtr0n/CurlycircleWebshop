import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './components/pages/contact/contact.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { MyOrdersComponent } from './components/pages/my-orders/my-orders.component';
import { ProductCategoriesComponent } from './components/pages/product-categories/product-categories.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { RegistrationComponent } from './components/pages/registration/registration.component';
import { UserGuard } from './guards/user.guard';

const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: 'profile', component: ProfileComponent, canActivate: [UserGuard] },
    { path: 'my-orders', component: MyOrdersComponent, canActivate: [UserGuard] },
    { path: 'product-categories', component: ProductCategoriesComponent },
    { path: 'contact', component: ContactComponent },
    { path: '', component: HomeComponent },
    { path: '**', component: HomeComponent }, //TODO: PageNotFoundComponent
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
