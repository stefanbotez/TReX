import { TrexModule } from "@framework";

import { OverviewPage } from "./pages";
import { ArticleComponent } from "./components";

@TrexModule({
    pages: [OverviewPage],
    components: [ArticleComponent]
})
export class OverviewModule {
}