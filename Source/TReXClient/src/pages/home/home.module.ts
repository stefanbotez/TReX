import { Module } from '@framework';
import { SidebarComponent, FilterBarComponent, NotFoundComponent } from '@components';
import { HomePage } from '@pages';


@Module({
    pages: [HomePage],
    components: [SidebarComponent, FilterBarComponent, NotFoundComponent]
})
export class HomeModule {
}