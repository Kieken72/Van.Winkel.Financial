
import {Error } from './error';
export class ApiError {
    errors: Error[];
    isValid: boolean;
    constructor(other: Partial<ApiError>) {
        this.errors = other.errors;
        this.isValid = other.isValid;
    }
}
