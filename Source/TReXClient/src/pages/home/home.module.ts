import { Module } from '@framework';
import { SidebarComponent, FilterBarComponent } from '@components';
import { HomePage } from '@pages';


@Module({
    pages: [HomePage],
    components: [SidebarComponent, FilterBarComponent]
})
export class HomeModule {
}