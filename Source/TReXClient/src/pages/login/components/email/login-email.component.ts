import * as template from './login-email.component.html';
import { TrexComponent, Component, HasInputs } from "@framework";
import { Subject } from 'rxjs';

@TrexComponent({
    selector: 'login-email',
    template: template
})
export class LoginEmailComponent extends Component implements HasInputs {
    private channel: Subject<string>;

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }
}