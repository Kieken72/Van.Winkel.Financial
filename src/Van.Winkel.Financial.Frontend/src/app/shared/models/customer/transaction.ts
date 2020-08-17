import { TransactionDto } from '../../api/dto';

export class Transaction {
  senderAccountId: string;
  amount: number;
  note: string;

  constructor(other?: Partial<Transaction>) {
    Object.assign(this, other);
  }
  static FromDto(dto: Partial<TransactionDto>) {
    return new Transaction({
      ...dto,
    });
  }
}
