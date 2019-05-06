import { HomePage, LoginPage, OverviewPage } from '@pages';

interface Route {
    page: any;
    redirectTo: any;
}

export const routes: {[s: string]: Partial<Route>} = {
    '/': {
        page: HomePage
    },
    '/login': {
        page: LoginPage
    },
    '/overview': {
        page: OverviewPage
    },
    '**': {
        page: HomePage
    }
};