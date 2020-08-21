import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Customer, ApiError } from '../shared';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {
  public formCustomer: Customer = new Customer();

  @Input() set customer(v: Customer) {
    this.formCustomer = new Customer(v);
  }

  @Input() error: ApiError;
  @Input() isNew: boolean;
  @Input() canEditData: boolean;

  @Output() save = new EventEmitter<Customer>();
  @Output() delete = new EventEmitter<Customer>();
  @Input() loaded = true;
  @Input() saveDisabled = false;

  constructor() { }

  ngOnInit(): void {
  }

}
