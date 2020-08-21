import { Component, OnInit } from '@angular/core';
import { CreateAccount, tapSuccess, mapError, AccountService } from '../shared';
import { Subject } from 'rxjs';
import { mergeMap, shareReplay } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent implements OnInit {

  private createAccount$ = new Subject<CreateAccount>();
  private createdAccount$ = this.createAccount$.pipe(
    mergeMap(_ => this.accountService.addAccount(_)),
    shareReplay(1));

  account: CreateAccount;

  error$ = this.createdAccount$.pipe(
    tapSuccess(({ customerId }) => this.router.navigateByUrl(`/customer/detail/${customerId}`)),
    mapError());

  constructor(private route: ActivatedRoute, private accountService: AccountService, private router: Router) {
    this.account = CreateAccount.Empty();
  }

  ngOnInit() {
    this.account.customerId = this.route.snapshot.paramMap.get('id');
  }

  createAccount(account: CreateAccount) {
    this.createAccount$.next(account);
  }
}
