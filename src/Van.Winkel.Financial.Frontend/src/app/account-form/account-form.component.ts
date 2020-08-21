import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {  ApiError, CreateAccount } from '../shared';

@Component({
  selector: 'app-account-form',
  templateUrl: './account-form.component.html',
  styleUrls: ['./account-form.component.css']
})
export class AccountFormComponent implements OnInit {

  public formAccount: CreateAccount = new CreateAccount();

  @Input() set account(v: CreateAccount) {
    this.formAccount = new CreateAccount(v);
  }

  @Input() error: ApiError;
  @Input() isNew: boolean;
  @Input() canEditData: boolean;

  @Output() save = new EventEmitter<CreateAccount>();
  @Output() delete = new EventEmitter<CreateAccount>();
  @Input() loaded = true;
  @Input() saveDisabled = false;

  constructor() { }

  ngOnInit(): void {
  }

}
