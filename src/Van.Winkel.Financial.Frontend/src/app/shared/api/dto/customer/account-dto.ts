import { TransactionDto } from '.';

export interface AccountDto {
  id: string;
  balance: number;
  transactions: TransactionDto[];
}


