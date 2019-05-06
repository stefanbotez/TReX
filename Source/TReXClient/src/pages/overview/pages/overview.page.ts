import * as template from './overview.page.html';
import { TrexPage, Page, DomMaster } from "@framework";

@TrexPage({
    template: template
})
export class OverviewPage extends Page {
    public titles: string[] = [
        "title1",
        "title2",
        "title3"
    ];

    public constructor(master: DomMaster) {
        super(master);
    }
}