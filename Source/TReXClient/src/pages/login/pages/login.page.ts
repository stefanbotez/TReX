import * as template from './login.page.html';
import { Subject } from 'rxjs';
import { 
    Page, 
    DomMaster, 
    TrexPage, 
    OnInit, 
    RouterService, 
    NotificationsService, 
    NotificationMessage,
    DomContainer
} from '@framework';

import { inject } from 'inversify';

@TrexPage({
    template: template
})
export class LoginPage extends Page implements OnInit {
    public emailChannel: Subject<string> = new Subject<string>();
    public passwordChannel: Subject<string> = new Subject<string>();

    public advancedToPassword = false;
    private email: string = '';
    private password: string = '';

    public constructor(
        @inject(RouterService) private routerService: RouterService,
        @inject(NotificationsService) private notifications: NotificationsService,
        @inject(DomMaster) master: DomMaster) {
        
        super(master);
    }

    public onInit(): void {
        this.initChannels();
    }

    public advance(): void {
        this.advancedToPassword = true;
    }

    private initChannels(): void {
        this.emailChannel.subscribe((email: string) => {
            this.email = email;
            this.advance();
        });

        this.passwordChannel.subscribe((password: string) => {
            this.password = password;
            this.routerService.goToHome();
        });
    }
}