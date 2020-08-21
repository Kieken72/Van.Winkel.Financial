import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { CustomerOverviewComponent } from './customer-overview/customer-overview.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { AddAccountComponent } from './add-account/add-account.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'customers'
  },
  {
    path: 'customers',
    component: CustomerOverviewComponent
  },
  {
    path: 'customer/detail/:id',
    component: CustomerComponent
  },
  {
    path: 'customer/add',
    component: AddCustomerComponent
  },
  {
    path: 'customer/edit/:id',
    component: EditCustomerComponent
  },
  {
    path: 'account/add',
    component: AddAccountComponent
  },
  {
    path: 'account/add/:id',
    component: AddAccountComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
