import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { CustomerOverviewComponent } from './customer-overview/customer-overview.component';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { FieldFormErrorComponent } from './field-form-error/field-form-error.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { AccountFormComponent } from './account-form/account-form.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    AddCustomerComponent,
    CustomerOverviewComponent,
    CustomerFormComponent,
    FieldFormErrorComponent,
    AddAccountComponent,
    AccountFormComponent,
    EditCustomerComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
