import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CreateAccount } from '../models';
import { catchValidationBag } from '..';
import { CreateAccountDto, OpenAccountDto } from './dto';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private httpClient: HttpClient) {

  }

  addAccount(account: CreateAccountDto) {
    const openAcc: OpenAccountDto = {
      initialCredit: account.balance
    };

    return this.httpClient
      .post<CreateAccountDto>(`api/account/${account.customerId}`, openAcc)
      .pipe(
        map(_ => CreateAccount.FromDto(_)),
        catchValidationBag()
      );
  }
}
