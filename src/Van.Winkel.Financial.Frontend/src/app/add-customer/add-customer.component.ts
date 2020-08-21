import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  CustomerService, Customer, tapSuccess, mapError,
} from 'src/app/shared';
import { _ParseAST } from '@angular/compiler';
import { Subject } from 'rxjs';
import { mergeMap, shareReplay, tap } from 'rxjs/operators';

@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {

  private createCustomer$ = new Subject<Customer>();
  private createdCustomer$ = this.createCustomer$.pipe(
    mergeMap(_ => this.cusomerService.addCustomer(_)),
    shareReplay(1));

  customer: Customer;

  error$ = this.createdCustomer$.pipe(
    tapSuccess(({ id }) => this.router.navigateByUrl(`/customer/detail/${id}`)),
    mapError());

  constructor(private cusomerService: CustomerService, private router: Router) {
    this.customer = Customer.Empty();
  }

  ngOnInit() {
  }

  createCustomer(customer: Customer) {
    this.createCustomer$.next(customer);
  }

}
