
import { CustomerDto } from '../../api/dto';
import { Account } from '.';

export class Customer {
  id: string;
  name: string;
  surname: string;
  accounts: Account[];
  detailLink: string;
  addAccountLink: string;

  constructor(other?: Partial<Customer>) {
    Object.assign(this, other);
    this.detailLink = `/customer/detail/${this.id}`;
    this.addAccountLink = `/account/add/${this.id}`;

  }
  static FromDto(dto: Partial<CustomerDto>) {
    return new Customer({
      ...dto,
      accounts: dto.accounts?.map(_ => Account.FromDto(_)),
      detailLink: `/customer/detail/${dto.id}`,
      addAccountLink: `/account/add/${dto.id}`
    });
  }

  static Empty(){
    return new Customer({
      name: '',
      surname: ''
    });
  }
}

