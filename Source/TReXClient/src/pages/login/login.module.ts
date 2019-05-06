import { TrexModule } from '@framework';

import { LoginPage } from './pages';
import { LoginEmailComponent, LoginPasswordComponent } from './components';

@TrexModule({
    pages: [LoginPage],
    components: [LoginEmailComponent, LoginPasswordComponent]
})
export class LoginModule {
}