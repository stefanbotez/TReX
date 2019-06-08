import * as template from './article-preview.component.html';
import { TrexComponent, Component, HasInputs } from "@framework";
import { Subject } from 'rxjs';


@TrexComponent({
    selector: 'article-preview',
    template: template
})
export class ArticlePreviewComponent extends Component implements HasInputs{
    private channel: Subject<string>;

    public title: string = "Test title";
    public content: string = "Test content asjdan  lajdslakj  lkajdalks asldk jalk jals kjsal jadj  lkajsd laj alkj laks jalk ";
    public tags: string[] = ["aaa", "bbb", "cccc"];

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
    }
}
