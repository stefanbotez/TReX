import { Form, FormField, RequiredValidator, EmailValidator } from "@framework";

export class LoginEmailForm extends Form {
    public email: string;

    protected getFields(): FormField<any>[] {
        const emailField = new FormField("email", this.email)
            .validatedBy(new RequiredValidator())
            .validatedBy(new EmailValidator());

        return [emailField];
    }
}