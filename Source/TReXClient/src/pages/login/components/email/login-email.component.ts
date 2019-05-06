import * as template from './login-email.component.html';
import { TrexComponent, Component, HasInputs, NotificationsService, NotificationMessage } from "@framework";
import { Subject } from 'rxjs';
import { LoginEmailForm } from './login-email.form';
import { inject } from 'inversify';

@TrexComponent({
    selector: 'login-email',
    template: template
})
export class LoginEmailComponent extends Component implements HasInputs {
    private channel: Subject<string>;
    public form: LoginEmailForm = new LoginEmailForm();

    public constructor(
        @inject(NotificationsService) private service: NotificationsService) {
        super();

        this.form.onFail.subscribe((errors: string[]) => {
            const messages = errors.map((e: string) => NotificationMessage.error(e));
            this.service.pushError(...messages);
        });

        this.form.onSuccess.subscribe(() => {
            this.channel.next(this.form.email);
        });
    }

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }

    public submit(e: any, self: LoginEmailComponent): void {
        self.form.submit();
    }
}