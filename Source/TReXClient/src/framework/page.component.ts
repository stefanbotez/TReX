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
}