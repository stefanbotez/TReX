import * as rivets from 'rivets';

interface ComponentParams {
    selector: string;
    template: string;
}

export function Component(params: ComponentParams): any {
    return (ctor: any) => {
        const selector = `trex-${params.selector}`;

        ctor.prototype.__selector = selector;
        ctor.prototype.__template = params.template;

        rivets.components[selector] = {
            template: () => params.template,
            initialize: (element, inputs) => {
                return new ctor().getBindingScope(element, inputs);
            }
        };
    }
}