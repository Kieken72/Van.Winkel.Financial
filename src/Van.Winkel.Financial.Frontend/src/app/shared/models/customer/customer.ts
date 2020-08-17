
import { CustomerDto } from '../../api/dto';
import { Account } from '.';

export class Customer {
  id: string;
  name: string;
  surname: string;
  accounts: Account[];

  constructor(other?: Partial<Customer>) {
    Object.assign(this, other);

  }
  static FromDto(dto: Partial<CustomerDto>) {
    return new Customer({
      ...dto,
      accounts: dto.accounts.map(_ => Account.FromDto(_)),
    });
  }
}
