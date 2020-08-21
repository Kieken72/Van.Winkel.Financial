import { CreateAccountDto } from '../../api/dto';

export class CreateAccount {
  customerId: string;
  balance: number;
  constructor(other?: Partial<CreateAccount>) {
    Object.assign(this, other);
  }


  static Empty() {
    return new CreateAccount({
      customerId: '',
      balance: 0.0,
    });
  }

  static FromDto(dto: Partial<CreateAccountDto>) {
    return new CreateAccount({
      ...dto
    });
  }
}
