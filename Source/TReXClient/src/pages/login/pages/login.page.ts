import * as template from './login.page.html';
import { Subject } from 'rxjs';
import { Page, DomMaster, TrexPage, OnInit } from '@framework';

class Credentials {
    public email: string;
    public password: string;
}

@TrexPage({
    template: template
})
export class LoginPage extends Page implements OnInit {
    private credentials = new Credentials();

    public emailChannel: Subject<string> = new Subject<string>();
    public passwordChannel: Subject<string> = new Subject<string>();

    public advancedToPassword = false;

    public constructor(master: DomMaster) {
        super(master);
    }

    public onInit(): void {
        this.emailChannel.subscribe((email: string) => {
            this.credentials.email = email;
            this.advance();
        });

        this.passwordChannel.subscribe((password: string) => {
            this.credentials.password = password;
            this.login();
        })
    }

    public advance(): void {
        this.advancedToPassword = true;
    }

    private login(): void {
        console.log('Login made for', this.credentials);
    }
}