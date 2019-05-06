import { Validator } from "./validator";
import { FormField } from "../form-field";
import { ValidationResult } from "./validation-result";

export class MinLengthValidator extends Validator {
    public constructor(private length: number){
        super();
    }

    public validate(field: FormField<any>): ValidationResult {
        if(field.value.length < this.length) {
            return ValidationResult.failure(`${field.capitalizedName} must be at least ${this.length} characters long!`);
        }

        return ValidationResult.successfull();
    }
}