import { Router } from "@framework";
import { routes } from "@constants";
import { LoginModule, OverviewModule } from "@pages";
import { LayoutModule } from "./layout/layout.module";

export class AppModule {
    private modules: any = [
        LoginModule,
        OverviewModule,
        LayoutModule
    ];    

    public static run(): void {
        window.addEventListener('hashchange', Router.routeFn(routes));
        window.addEventListener('load', Router.routeFn(routes));
    }
}