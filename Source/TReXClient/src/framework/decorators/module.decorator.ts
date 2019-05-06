import { PageConstructor, ComponentConstructor } from "@framework";

interface ModuleParams {
    pages: any[];
    components: ComponentConstructor[];
}

export function TrexModule(params: ModuleParams): any {
    return (ctor: any) => {
    };
}