import * as template from './article-preview.component.html';
import { TrexComponent, Component, HasInputs, OnInit } from "@framework";
import { Subject } from 'rxjs';


@TrexComponent({
    selector: 'article-preview',
    template: template
})
export class ArticlePreviewComponent extends Component implements HasInputs{
    private channel: Subject<string>;

    public title: string;
    public content: string;
    public tags: string[];

    public gatherInputs(inputs: any): void {
        this.channel = inputs.channel;
        this.title = inputs.article.title;
        this.content = inputs.article.text;
        this.tags = inputs.article.tags;
    }
}
