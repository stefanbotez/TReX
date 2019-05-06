import * as template from './sidebar.component.html';
import { TrexComponent, Component } from "@framework";

@TrexComponent({
    selector: 'sidebar',
    template: template
})
export class SidebarComponent extends Component {

}