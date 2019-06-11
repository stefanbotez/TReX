import * as template from './sidebar.component.html';
import { TrexComponent, Component, HasInputs } from "@framework";
import { Subject } from 'rxjs';


@TrexComponent({
    selector: 'sidebar',
    template: template
})
export class SidebarComponent extends Component implements HasInputs{
    private channel: Subject<string>;
    public name: string = "Giurgila Alexandru";
    public initials: string = "GA";
    public occupation: string = "Student";

    isHistoryActive: boolean = true;
    isSavedActive: boolean = false;
    isProposalsActive: boolean = false;

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }

    public selected(clickedElement: any, self: SidebarComponent) {
        if (clickedElement.path[0].innerText === 'HISTORY') {
            if(self.isHistoryActive === false) {
                self.channel.next(clickedElement.path[0].innerText);
                self.isHistoryActive = true;
                self.isProposalsActive = false;
                self.isSavedActive = false;
            }
        }

        if (clickedElement.path[0].innerText === 'SAVED') {
            if(self.isSavedActive === false) {
                self.channel.next(clickedElement.path[0].innerText);
                self.isHistoryActive = false;
                self.isProposalsActive = false;
                self.isSavedActive = true;
            }
        }

        if (clickedElement.path[0].innerText === 'PROPOSALS') {
            if(self.isProposalsActive === false) {
                self.channel.next(clickedElement.path[0].innerText);
                self.isHistoryActive = false;
                self.isProposalsActive = true;
                self.isSavedActive = false;
            }
        }
    }
}
