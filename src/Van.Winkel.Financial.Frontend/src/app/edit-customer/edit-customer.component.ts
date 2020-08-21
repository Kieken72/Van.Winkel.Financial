import { Component, OnInit } from '@angular/core';
import { Customer, mapLoading, liftLoaded, liftValue, mapError, CustomerService, mapSuccess, tapSuccess } from '../shared';
import { Subject, Observable, merge } from 'rxjs';
import { shareReplay, map, filter } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit {
  private updateCustomer$ = new Subject<Customer>();
  private reload$ = new Subject<Customer>();

  private updatingCustomer$ = this.updateCustomer$.pipe(
      mapLoading(
          (_) => this.cusomerService.updateCustomer(_.id, _)
      ),
  );

  error$ = this.updatingCustomer$.pipe(
    liftValue(),
  tapSuccess(({ id }) => this.router.navigateByUrl(`/customer/detail/${id}`))
  , mapError());

  tableSaving$ = this.updatingCustomer$.pipe(
      liftLoaded(0),
      shareReplay(1),
      map((_) => !_),
  );
  customer$: Observable<Customer>;
  loaded$: Observable<boolean>;

  constructor(
      private cusomerService: CustomerService,
      route: ActivatedRoute,
      private router: Router,
  ) {
      const customerLoading$ = merge(
          route.paramMap.pipe(map((_) => _.get('id'))),
          this.reload$.pipe(map((_) => _.id)),
      ).pipe(
          mapLoading(
              (id) => cusomerService.getCustomer(id)
          ),
          shareReplay(1)
      );

      this.customer$ = merge(
        customerLoading$.pipe(
              liftValue(),
              mapSuccess()
          ),
          this.updatingCustomer$.pipe(
              liftValue(),
              mapSuccess(),
              filter((_) => !!_),
          )
      )

      this.loaded$ = customerLoading$.pipe(liftLoaded());

  }

  ngOnInit() {}

  updateCustomer(customer: Customer) {
      this.updateCustomer$.next(customer);
  }
}
