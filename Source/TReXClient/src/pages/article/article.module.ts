import { Module } from '@framework';
import { SidebarComponent, FilterBarComponent } from '@components';
import { ArticlePage } from '@pages';


@Module({
    pages: [ArticlePage],
    components: [SidebarComponent, FilterBarComponent]
})
export class ArticleModule {
}