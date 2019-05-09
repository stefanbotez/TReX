import { HomePage, LoginPage } from '@pages';

export interface Route {
    page: any;
    redirectTo: any;
}

export const routes: {[s: string]: Partial<Route>} = {
    '/home': {
        page: HomePage
    },
    '/login': {
        page: LoginPage
    },
    '*': {
        redirectTo: '/home'
    },
};