import { LoginModule } from "@pages";
import { Router } from "@framework";
import { routes } from "@constants";

export class AppModule {
    private modules: any = [
        LoginModule
    ];    

    public static run(): void {
        Router.init(routes);
    }
}