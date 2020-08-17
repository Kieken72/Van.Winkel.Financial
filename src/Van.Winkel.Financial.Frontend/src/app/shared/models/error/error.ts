import { ValidationErrorCode } from '../enums';
import { Parameters } from './parameters';

export interface Error {
    error: ValidationErrorCode;
    parameters: Parameters;
}
