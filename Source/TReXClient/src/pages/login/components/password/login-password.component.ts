import * as template from './login-password.component.html';
import { Component, TrexComponent } from "@framework";

@TrexComponent({
    selector: 'login-password',
    template: template
})
export class LoginPasswordComponent extends Component {
}