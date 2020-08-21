import { ValidationErrorCode } from '../enums';
import { Parameters } from './parameters';

export interface Error {
    validationErrorCode: ValidationErrorCode;
    namedParameters: Parameters;
}
