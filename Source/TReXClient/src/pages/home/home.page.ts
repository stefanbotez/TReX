import { injectable } from 'inversify';
import { PageComponent, Page, DomMaster } from '@framework';

import * as template from './home.page.html'

@injectable()
@Page({
    template: template
})
export class HomePage extends PageComponent {
    public value = 282;
    
    public constructor(master: DomMaster) {
        super(master);
    }
}