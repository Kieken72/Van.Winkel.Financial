import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  CustomerService, mapLoading, Customer, ApiError, mapError, liftValue, mapSuccess, liftLoaded,
} from 'src/app/shared';
import { map, shareReplay, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-customer-overview',
  templateUrl: './customer-overview.component.html',
  styleUrls: ['./customer-overview.component.css']
})
export class CustomerOverviewComponent implements OnInit {

  error$: Observable<ApiError>;
  loaded$: Observable<boolean>;
  customers$: Observable<Customer[]>;

  constructor(route: ActivatedRoute, private customerService: CustomerService) {

    const customersLoading$ = route.paramMap.pipe(
      mapLoading(
        id => customerService.getCustomers()
      ),
      shareReplay(1)
    );

    this.error$ = customersLoading$.pipe(
      mapError(),
      shareReplay(1)
    );

    this.customers$ = customersLoading$.pipe(
      liftValue(),
      mapSuccess());

    const customersLoaded$ = customersLoading$.pipe(liftLoaded());

  }

  ngOnInit(): void {
  }

}
