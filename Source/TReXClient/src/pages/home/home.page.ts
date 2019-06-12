import { injectable } from 'inversify';
import { TrexPage, DomMaster, Page, OnInit } from '@framework';

import * as template from './home.page.html'
import { Subject } from 'rxjs';

@TrexPage({
    template: template
})
export class HomePage extends Page implements OnInit {
    public filterbarChannel: Subject<any> = new Subject<any>();
    public sidebarChannel: Subject<string> = new Subject<string>();
    public proposalChannel: Subject<string> = new Subject<string>();
    public notFound: boolean = false;
    public historyTabSelected: boolean = true;
    public savedTabSelected: boolean = false;
    public proposalsTabSelected: boolean = false;
    private localOrderByValue: string = 'asc';
    private localSearchValue: string = '';
    private localFilterByValue: string = '';
    //temporary
    //just for testing
    public articles: any[] = [
        {
            title: 'Best title 1',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['aaa', 'bbb'],
            id: '1'
        },
        {
            title: 'My test title',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['aaa', 'ccc', 'ddd'],
            id: '3'
        },
        {
            title: 'Another best title',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['aaa', 'bbb'],
            id: '1'
        },
        {
            title: 'Best title 2',
            text: 'asdjhakdj ak hjaksj hdka haksjda hakjsajdaj hkaj hdoiau doiduiwrbwbrf e b o jasop aknd ak dan ahd uiahdaid jajd nas d',
            tags: ['bbb', 'ccc', 'ddd'],
            id: '2'
        }
    ];
    public youtubeArticles: any[] = [
        {
            title: "My youtube video",
            thumbnail: "aaa",
            description: "Describe your channel. This might be the most obvious thing to do, but it’s where most people get hung up. The goal of this description is to tell your viewers what will happen if they subscribe and watch your videos. What kind of content will they see? How frequently will they see it? Will they learn anything? Make sure they know the benefits of subscribing such as your amazing sense of humor or your easy to understand tutorials.",
            tags: ['aaa','bbb'],
            id: 'y1'
        }
    ];

    public vimeoArticles: any[] = [
        {
            title: "My vimeo video",
            thumbnail: "aaa",
            description: "Describe your channel. This might be the most obvious thing to do, but it’s where most people get hung up. The goal of this description is to tell your viewers what will happen if they subscribe and watch your videos. What kind of content will they see? How frequently will they see it? Will they learn anything? Make sure they know the benefits of subscribing such as your amazing sense of humor or your easy to understand tutorials.",
            tags: ['ccc','aaa'],
            id: 'v1'
        }
    ]

    public twitterArticles: any[] = [
        {
            title: "My twiitter article",
            description: "With Twitter, it wasn't clear what it was. They called it a social network, they called it microblogging, but it was hard to define, because it didn't replace anything. There was this path of discovery with something like that, where over time you figure out what it is. Twitter actually changed from what we thought it was in the beginning, which we described as status updates and a social utility. It is that, in part, but the insight we eventually came to was Twitter was really more of an information network than it is a social network.",
            tags: ['aaaa', 'vvv'],
            id: 't1'
        }
    ]

    public updatedArticles: any[];

    public constructor(master: DomMaster) {
        super(master);
    }

    public onInit(): void {
        this.updatedArticles = this.articles;
        this.initChannels();
        this.orderArticlePreviews(this.localOrderByValue);
    }

    private initChannels(): void {
        this.filterbarChannel.subscribe((data: any) => {
            if (data.type === 'search') {
                this.localSearchValue = data.value;
                this.filterArticlePreviewsBySearch(this.localSearchValue);
            } else {
                if (data.type === 'orderBy') {
                    this.localOrderByValue = data.value;
                    this.orderArticlePreviews(this.localOrderByValue);
                } else {
                    this.localFilterByValue = data.value;
                    this.filterArticlePreviewsByFilter(this.localFilterByValue);
                }
            }
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

    private filterArticlePreviewsBySearch(searchValue: string) {
        this.updatedArticles = [];
        this.updatedArticles = this.articles.filter(x => {
            return x.title.toLowerCase().includes(searchValue.toLowerCase());
        });

        this.notFound = this.updatedArticles.length === 0 ? true: false;
        this.orderArticlePreviews(this.localOrderByValue);
    }

    private filterArticlePreviewsByFilter(filterByValue: string) {
        this.filterArticlePreviewsBySearch(this.localSearchValue);
        if (filterByValue !== '') {
            const tempUpdatedArticles = this.updatedArticles; 
            this.updatedArticles = [];
            this.updatedArticles = tempUpdatedArticles.filter(x => {
                return x.tags.includes(filterByValue);
            });
        }
    }

    private orderArticlePreviews(orderByValue: string) {
        const tempUpdatedArticles = this.updatedArticles; 
        this.updatedArticles = [];
        this.updatedArticles = tempUpdatedArticles.sort((a, b) => {
            if (orderByValue === 'asc') {
                if(a.title > b.title) {
                return 1;
                } else if(a.title < b.title) {
                return -1;
                } else {
                return 0;
                }
            } else {
                if(a.title < b.title) {
                return 1;
                } else if(a.title > b.title) {
                return -1;
                } else {
                return 0;
                }
            }
        });
    }
}