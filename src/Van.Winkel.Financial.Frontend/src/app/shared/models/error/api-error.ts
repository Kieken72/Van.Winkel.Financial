import { ValidationBag } from './validation-bag';

export class ApiError {
    validationBag: ValidationBag;
    constructor(other: Partial<ApiError>) {
        this.validationBag = other.validationBag;
    }
}
