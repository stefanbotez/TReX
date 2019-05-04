import { injectable } from 'inversify';
import { TrexPage, DomMaster, Page } from '@framework';

import * as template from './home.page.html'

@TrexPage({
    template: template
})
export class HomePage extends Page {
    public value = 282;
    
    public constructor(master: DomMaster) {
        super(master);
    }
}