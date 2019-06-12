import { injectable } from 'inversify';
import { TrexPage, DomMaster, Page, OnInit } from '@framework';

import * as template from './home.page.html'
import { Subject } from 'rxjs';

@TrexPage({
    template: template
})
export class HomePage extends Page implements OnInit {
    public filterbarChannel: Subject<string> = new Subject<string>();
    public sidebarChannel: Subject<string> = new Subject<string>();
    public proposalChannel: Subject<string> = new Subject<string>();
    public notFound: boolean = false;
    public historyTabSelected: boolean = true;
    public savedTabSelected: boolean = false;
    public proposalsTabSelected: boolean = false;
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
            title: 'My test title 11',
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

    public updatedArticles: any[];

    public constructor(master: DomMaster) {
        super(master);
    }

    public onInit(): void {
        this.updatedArticles = this.articles;
        this.initChannels();
    }

    private initChannels(): void {
        this.filterbarChannel.subscribe((value: string) => {
            this.filterArticlePreviews(value);
        });

        this.proposalChannel.subscribe((value: string) => {
            //handle form input - make request
        });

        this.sidebarChannel.subscribe((value: string) => {
            if (value === 'HISTORY') {
                this.historyTabSelected = true;
                this.savedTabSelected = false;
                this.proposalsTabSelected = false;
            }

            if (value === 'SAVED') {
                this.historyTabSelected = false;
                this.savedTabSelected = true;
                this.proposalsTabSelected = false;
            }

            if (value === 'PROPOSALS') {
                this.historyTabSelected = false;
                this.savedTabSelected = false;
                this.proposalsTabSelected = true;
            }
        })
    }

    private filterArticlePreviews(searchValue: string) {
        this.updatedArticles = [];
        this.updatedArticles = this.articles.filter(x => {
            return x.title.toLowerCase().includes(searchValue.toLowerCase());
        });

        this.notFound = this.updatedArticles.length === 0 ? true: false;
    }
}