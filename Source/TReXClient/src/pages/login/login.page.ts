import * as template from './login.page.html';
import { Page, PageComponent, DomMaster } from '@framework';
import { injectable } from 'inversify';

@injectable()
@Page({
    template: template
})
export class LoginPage extends PageComponent {
    public constructor(master: DomMaster) {
        super(master);
    }
}