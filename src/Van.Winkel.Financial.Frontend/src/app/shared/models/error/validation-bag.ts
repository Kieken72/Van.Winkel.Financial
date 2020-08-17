import { Error } from './error';

export interface ValidationBag {
    errors: Error[];
    isValid: boolean;
    updatedRows: [];
}
