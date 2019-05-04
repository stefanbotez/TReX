import { LoginModule } from "@pages";
import { Router } from "@framework";
import { routes } from "@constants";

export class AppModule {
    private modules: any = [
        LoginModule
    ];    

    public static run(): void {
        window.addEventListener('hashchange', Router.routeFn(routes));
        window.addEventListener('load', Router.routeFn(routes));
    }
}