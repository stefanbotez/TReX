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

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }
}
