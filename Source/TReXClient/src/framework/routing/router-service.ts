import { injectable } from "inversify";

@injectable()
export class RouterService {
    public goToHome(): void {
        this.goTo('/home');
    }
    
    public goTo(route: string): void {
        window.location.hash = route;
    }   
}