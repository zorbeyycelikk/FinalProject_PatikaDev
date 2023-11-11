import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { HeaderComponent } from './main/header/header.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { httpInterceptorProviders } from './helpers/token.interceptor';
import { ProductComponent } from './main/product/product.component';
import { BasketComponent } from './main/basket/basket.component';
import { SettingsComponent } from './main/settings/settings.component';
import { OrdersComponent } from './main/orders/orders.component';
import { MessagesComponent } from './main/messages/messages.component';
import { LiveSupportComponent } from './main/live-support/live-support.component';
import { CustomerComponent } from './main/customer/customer.component';
import { SpinnersComponent } from './spinners/spinners.component';
import {ProductFilterPipe} from './main/product/product-filter.pipe'
import { FormsModule } from '@angular/forms';
import { CustomerFilterPipe } from './main/customer/customer-filter.pipe';
import { UserInformationsComponent } from './main/settings/user-informations/user-informations.component';
import { AccountInformationsComponent } from './main/settings/account-informations/account-informations.component';





@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    HeaderComponent,
    ProductComponent,
    BasketComponent,
    SettingsComponent,
    OrdersComponent,
    MessagesComponent,
    LiveSupportComponent,
    CustomerComponent,
    SpinnersComponent,
    ProductFilterPipe,
    CustomerFilterPipe,
    UserInformationsComponent,
    AccountInformationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [AuthService , httpInterceptorProviders], 
  bootstrap: [AppComponent]
})
export class AppModule { }
