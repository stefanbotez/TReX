import * as template from './login.page.html';
import { Subject } from 'rxjs';
import { Page, DomMaster, TrexPage, OnInit, RouterService } from '@framework';
import { inject } from 'inversify';
import { LoginForm } from './login.form';

@TrexPage({
    template: template
})
export class LoginPage extends Page implements OnInit {
    public emailChannel: Subject<string> = new Subject<string>();
    public passwordChannel: Subject<string> = new Subject<string>();
    public form: LoginForm = new LoginForm();

    public advancedToPassword = false;

    public constructor(
        @inject(RouterService) private routerService: RouterService,
        @inject(DomMaster) master: DomMaster) {
        
        super(master);
    }

    public onInit(): void {
        this.initChannels();
        this.initForm();
    }

    public advance(): void {
        this.advancedToPassword = true;
    }

    private initChannels(): void {
        this.emailChannel.subscribe((email: string) => {
            this.form.email = email;
            this.advance();
        });

        this.passwordChannel.subscribe((password: string) => {
            this.form.password = password;
            this.form.submit();
        });
    }

    private initForm(): void {
        this.form.onSuccess.subscribe(() => {
            alert(`Login made for credentials: ${this.form.email}, ${this.form.password}`);
            this.routerService.goToHome();
        });
        this.form.onFail.subscribe((errors: string[]) => console.log("errors m8", errors));
    }
}