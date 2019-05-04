import { PageConstructor, ComponentConstructor } from "@framework";

interface ModuleParams {
    pages: PageConstructor[];
    components: ComponentConstructor[];
}

export function Module(params: ModuleParams): any {
    return (ctor: any) => {
    };
}