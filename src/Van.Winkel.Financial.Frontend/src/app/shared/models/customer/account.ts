
import { Transaction } from '.';
import { AccountDto } from '../../api/dto';

export class Account {
  id: string;
  balance: number;
  transactions: Transaction[];
  constructor(other?: Partial<Account>) {
    Object.assign(this, other);
  }
  static FromDto(dto: Partial<AccountDto>) {
    return new Account({
      ...dto,
      transactions: dto.transactions.map(_ => Transaction.FromDto(_)),
    });
  }
}


