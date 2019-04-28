import { Container } from 'inversify';
import { DomContainer } from '@framework/dom-container';
import { DomMaster } from '@framework/dom-master';

const container = new Container();
container.bind<DomMaster>(DomMaster).toSelf();
container.bind<DomContainer>(DomContainer).toSelf();

export const trexContainer = container;