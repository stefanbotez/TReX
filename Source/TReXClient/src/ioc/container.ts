import { Container } from 'inversify';
import { DomMaster} from '@framework/dom';
import { DomContainer } from '@framework/dom';

const container = new Container();
container.bind<DomMaster>(DomMaster).toSelf();
container.bind<DomContainer>(DomContainer).toSelf();

export const trexContainer = container;