import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { AuthGuardService } from './services/auth-guard.service';
import { ProductComponent } from './main/product/product.component';
import { HeaderComponent } from './main/header/header.component';
import { BasketComponent } from './main/basket/basket.component';
import { SettingsComponent } from './main/settings/settings.component';
import { OrdersComponent } from './main/orders/orders.component';
import { MessagesComponent } from './main/messages/messages.component';
import { LiveSupportComponent } from './main/live-support/live-support.component';
import { CustomerComponent } from './main/customer/customer.component';
import { UserInformationsComponent } from './main/settings/user-informations/user-informations.component';
import { AccountInformationsComponent } from './main/settings/account-informations/account-informations.component';



//  const routes: Routes = [
//    { path: 'login', component: LoginComponent },
//    { path: 'main', component: MainComponent,children: [
//        { path: '', redirectTo: 'product', pathMatch: 'full' },
//        { path: 'product', component: ProductComponent },
//        { path: 'basket', component: BasketComponent },
//        { path: 'header', component: HeaderComponent },
//        { path: 'settings', component: SettingsComponent },
//        { path: 'orders', component: OrdersComponent },
//        { path: 'messages', component: MessagesComponent },
//        { path: 'live-support', component: LiveSupportComponent },
//       { path: 'customer', component: CustomerComponent },

//      ]
//    },
//    { path: '', redirectTo: 'main', pathMatch: 'full' },
//    // Diğer route'larınızı buraya ekleyin
//  ]

  const routes: Routes = [
   { path: 'login', component: LoginComponent },
   { path: 'main', component: MainComponent, canActivate: [AuthGuardService], children: [
       { path: '', redirectTo: 'product', pathMatch: 'full' },
       { path: 'product', canActivate: [AuthGuardService], component: ProductComponent },
       { path: 'basket', canActivate: [AuthGuardService], component: BasketComponent },
       { path: 'header', canActivate: [AuthGuardService], component: HeaderComponent },
       { path: 'settings', canActivate: [AuthGuardService], component: SettingsComponent , children : [
        {path:'user-informations',canActivate:[AuthGuardService] ,component:UserInformationsComponent },
        {path:'account-informations',canActivate:[AuthGuardService] ,component:AccountInformationsComponent },
       ]},
       { path: 'orders', canActivate: [AuthGuardService], component: OrdersComponent },
       { path: 'messages', canActivate: [AuthGuardService], component: MessagesComponent },
       { path: 'live-support', canActivate: [AuthGuardService], component: LiveSupportComponent },
       { path: 'customer', canActivate: [AuthGuardService], component: CustomerComponent },
     ]
   },
   { path: '', redirectTo: 'main', pathMatch: 'full' },
   // Diğer route'larınızı buraya ekleyin
 ]


@NgModule({
  imports: [RouterModule.forRoot(routes , { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
