import * as template from './login.page.html';
import { Subject } from 'rxjs';
import { Page, DomMaster, TrexPage } from '@framework';

@TrexPage({
    template: template
})
export class LoginPage extends Page {
    public emailChannel: Subject<string> = new Subject<string>();
    public passwordChannel: Subject<string> = new Subject<string>();

    public advancedToPassword = true;

    public constructor(master: DomMaster) {
        super(master);
    }

    public advance(): void {
        this.advancedToPassword = true;
    }
}