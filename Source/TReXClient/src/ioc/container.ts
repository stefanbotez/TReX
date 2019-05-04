import { Container } from 'inversify';
import { DomMaster} from '@framework/dom';
import { DomContainer } from '@framework/dom';
import { RouterService } from '@framework/routing';

const container = new Container();
container.bind<DomMaster>(DomMaster).toSelf();
container.bind<DomContainer>(DomContainer).toSelf();
container.bind<RouterService>(RouterService).toSelf();

export const trexContainer = container;