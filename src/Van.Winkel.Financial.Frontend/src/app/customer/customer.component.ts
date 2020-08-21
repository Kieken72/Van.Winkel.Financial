import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  CustomerService, mapLoading, Customer, ApiError, mapError, liftValue, mapSuccess, liftLoaded,
} from 'src/app/shared';
import { map, shareReplay, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  error$: Observable<ApiError>;
  loaded$: Observable<boolean>;
  customer$: Observable<Customer>;

  constructor(route: ActivatedRoute, private customerService: CustomerService) {

    const customerLoading$ = route.paramMap.pipe(
      map(_ => _.get('id')),
      mapLoading(
        id => customerService.getCustomer(id)
      ),
      shareReplay(1)
    );

    this.error$ = customerLoading$.pipe(
      mapError(),
      shareReplay(1)
    );

    this.customer$ = customerLoading$.pipe(
      liftValue(),
      mapSuccess());

    const customerLoaded$ = customerLoading$.pipe(liftLoaded());

  }

  ngOnInit(): void {
  }

}
