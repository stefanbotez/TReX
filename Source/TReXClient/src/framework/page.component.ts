import { injectable } from 'inversify';
import { DomMaster } from './dom-master';

@injectable()
export abstract class PageComponent {
    public readonly __template: string;

    protected constructor(private readonly master: DomMaster) {
    }

    public render(): void {
        this.master.bindPage(this);
    }

    public get tag() {
        const pageName = this.constructor.name.replace('Page', '');
        const tag = pageName[0].toLowerCase() + pageName.substring(1);

        return tag.replace(/([A-Z])/g, (g) => `-${g[0].toLowerCase()}`);
    }
}