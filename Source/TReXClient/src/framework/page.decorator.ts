interface PageParams {
    template: string;
}

export function Page(params: PageParams): any {
    return (ctor: any) => {
        ctor.prototype.__template = params.template;
    }
}