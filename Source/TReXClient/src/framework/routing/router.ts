import { trexContainer } from '@ioc';

export class Router {

    public static routeFn(routes: any): () => void {
        return (): void => {
            const request = Router.parseRequestURL()
            const parsedURL = (request.resource ? '/' + request.resource : '/') + (request.id ? '/:id' : '') + (request.verb ? '/' + request.verb : '')
    
            const route = routes[parsedURL] ? routes[parsedURL] : routes['**'];
            if(!route) {
                return;
            }
            
            const pageInstance: any = trexContainer.resolve(route.page);
            pageInstance.render();
        };
    }

    private static parseRequestURL() {

        let url = location.hash.slice(1).toLowerCase() || '/';
        let r = url.split("/")
        let request = {
            resource    : null,
            id          : null,
            verb        : null
        }
        request.resource    = r[1]
        request.id          = r[2]
        request.verb        = r[3]

        return request
    }
}