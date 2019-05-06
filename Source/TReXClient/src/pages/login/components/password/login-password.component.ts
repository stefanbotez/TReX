import * as template from './login-password.component.html';
import { Component, TrexComponent, HasInputs, NotificationsService, NotificationMessage } from "@framework";
import { Subject } from 'rxjs';
import { LoginPasswordForm } from './login-password.form';
import { inject } from 'inversify';

@TrexComponent({
    selector: 'login-password',
    template: template
})
export class LoginPasswordComponent extends Component implements HasInputs {
    private channel: Subject<string>;
    public form: LoginPasswordForm = new LoginPasswordForm();

    public constructor( 
        @inject(NotificationsService) private service: NotificationsService) {
        super();

        this.form.onSuccess.subscribe(() => {
            this.channel.next(this.form.password);
        });

        this.form.onFail.subscribe((errors: string[]) => {
            const messages = errors.map((e: string) => NotificationMessage.error(e));
            this.service.pushError(...messages);
        });
    }

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }

    public submit(e: any, self: LoginPasswordComponent): void {
        self.form.submit();
    }
}