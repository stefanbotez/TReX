import * as rivets from 'rivets';
import { injectable } from 'inversify';
import { PageComponent } from './page.component';
import { DomContainer } from './dom-container';

@injectable()
export class DomMaster {
    private currentView: any;

    public constructor(private readonly container: DomContainer) {
    }

    public bindPage(page: PageComponent): void {
        if (this.currentView) {
            this.currentView.unbind();
        }
        
        this.container.$element.innerHTML = page.__template;
        this.container.$element.id = page.tag;

        this.currentView = rivets.bind(this.container.$element, page);
    }
}