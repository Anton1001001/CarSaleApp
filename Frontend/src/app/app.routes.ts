import { Routes } from '@angular/router';
import { NewAdComponent } from './adverts/feature/new-ad/new-ad.component';
import { CarListingComponent } from './adverts/feature/car-listing/car-listing.component';
import { MotoListingComponent } from './adverts/feature/moto-listing/moto-listing.component';
import { BusListingComponent } from './adverts/feature/bus-listing/bus-listing.component';
import { TruckListingComponent } from './adverts/feature/truck-listing/truck-listing.component';
import { HomeComponent } from './home/feature/home.component';
import { LoginComponent } from './accounts/feature/login/login.component';
import { RegisterComponent } from './accounts/feature/register/register.component';
import { PlayComponent } from './play/play.component';
import { authorizationGuard } from './shared/guards/authorization.guard';
import { ConfirmEmailComponent } from './accounts/feature/confirm-email/confirm-email.component';
import { SendEmailComponent } from './accounts/feature/send-email/send-email.component';
import { ResetPasswordComponent } from './accounts/feature/reset-password/reset-password.component';
import { AdvertDetailsComponent } from './adverts/feature/advert-details/advert-details.component';
import { PrintComponent } from './print/feature/print/print.component';

export const routes: Routes = [
    { path: '', component: HomeComponent, title: 'Главная страница'},
    { 
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authorizationGuard],
        children: [
            { path: 'play', component: PlayComponent }
        ]
    },
    { path: 'login', component: LoginComponent, title: 'Вход'},
    {path: 'print', component: PrintComponent, title: 'Печать'},
    { path: 'register', component: RegisterComponent, title: 'Регистрация'},
    { path: 'home', component: HomeComponent, title: 'Главная страница'},
    { path: 'confirm-email', component: ConfirmEmailComponent},
    { path: 'send-email/:mode', component: SendEmailComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    { path: 'advert-details/:id', component: AdvertDetailsComponent },
    {
        path: 'create',
        runGuardsAndResolvers: 'always',
        canActivate: [authorizationGuard],
        component: CarListingComponent,
        title: 'create',
        // children: [
        //   { path: 'cars', component: CarListingComponent },
        //   { path: 'moto', component: MotoListingComponent },
        //   { path: 'buses', component: BusListingComponent },
        //   { path: 'trucks', component: TruckListingComponent }
        // ]
      }
      
];
