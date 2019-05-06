import * as template from './article.component.html';
import { Component, TrexComponent, HasInputs } from "@framework";

@TrexComponent({
    selector: 'article',
    template: template
})
export class ArticleComponent extends Component implements HasInputs {
    public titleInput: string;

    public gatherInputs(inputs: any): void {
        this.titleInput = inputs.titleinput;
    }
}