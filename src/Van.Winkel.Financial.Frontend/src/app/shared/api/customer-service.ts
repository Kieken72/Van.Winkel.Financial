import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { Customer } from '../models';
import { catchValidationBag } from '..';
import { CustomerDto } from './dto';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private httpClient: HttpClient) {
  }
  getCustomers() {
    return this.httpClient
      .get<CustomerDto[]>('api/customer/')
      .pipe(
        map(_ => _.map(c => Customer.FromDto(c)))
      );
  }

  getCustomer(id: string) {
    return this.httpClient
      .get<CustomerDto>('api/customer/' + id)
      .pipe(
        map(_ => Customer.FromDto(_)),
        catchValidationBag()
      );
  }

  updateCustomer(id: string, customer: CustomerDto) {
    return this.httpClient
      .put<CustomerDto>('api/customer/' + id, customer)
      .pipe(
        map(_ => Customer.FromDto(_)),
        catchValidationBag()
      );
  }

  addCustomer(customer: CustomerDto) {
    return this.httpClient
      .post<CustomerDto>('api/customer/', customer)
      .pipe(
        map(_ => Customer.FromDto(_)),
        catchValidationBag()
      );
  }
}
