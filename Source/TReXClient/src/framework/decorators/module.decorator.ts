import { PageConstructor, ComponentConstructor } from "@framework";

interface ModuleParams {
    pages: any[];
    components: ComponentConstructor[];
}

export function Module(params: ModuleParams): any {
    return (ctor: any) => {
    };
}