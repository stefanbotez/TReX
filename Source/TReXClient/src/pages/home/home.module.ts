import { Module } from '@framework';
import { SidebarComponent, FilterBarComponent, NotFoundComponent } from '@components';
import { HomePage } from '@pages';
import { ProposalComponent } from './components';


@Module({
    pages: [HomePage],
    components: [SidebarComponent, FilterBarComponent, NotFoundComponent, ProposalComponent]
})
export class HomeModule {
}