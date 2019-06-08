import { injectable } from 'inversify';
import { TrexPage, DomMaster, Page, OnInit } from '@framework';

import * as template from './home.page.html'
import { Subject } from 'rxjs';

@TrexPage({
    template: template
})
export class HomePage extends Page {
    //temporary
    //just for testing
    public articles: any[] = [
        {
            title: 'My test title 1',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['aaa', 'bbb'],
            id: '1'
        },
        {
            title: 'My test title 2',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['bbb', 'ccc', 'ddd'],
            id: '2'
        },
        {
            title: 'My test title 3',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['aaa', 'ccc', 'ddd'],
            id: '3'
        }
    ];

    public constructor(master: DomMaster) {
        super(master);
    }
}