import { Form, FormField, RequiredValidator, MinLengthValidator } from "@framework";

export class LoginPasswordForm extends Form {
    public password: string;

    protected getFields(): FormField<any>[] {
        const passwordField = new FormField<string>("password", this.password)
            .validatedBy(new RequiredValidator())
            .validatedBy(new MinLengthValidator(5));
        
        return [passwordField];
    }
}