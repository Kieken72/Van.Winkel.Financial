import { AccountDto } from '.';

export interface CustomerDto {
  id: string;
  name: string;
  surname: string;
  accounts: AccountDto[];
}
