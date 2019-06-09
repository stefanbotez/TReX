import * as template from './filter-bar.component.html';
import { TrexComponent, Component, HasInputs } from "@framework";
import { Subject } from 'rxjs';


@TrexComponent({
    selector: 'filter-bar',
    template: template
})
export class FilterBarComponent extends Component implements HasInputs {
    public inputValue: string = '';
    private channel: Subject<string>;
    
    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }

    public search(e: any, self: FilterBarComponent): void {
        self.channel.next(self.inputValue);
    }
}
