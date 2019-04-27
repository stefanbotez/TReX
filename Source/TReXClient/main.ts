import 'reflect-metadata';
import { Router } from '@framework';
import { routes } from '@constants'

// Listen on hash change:
window.addEventListener('hashchange', Router.routeFn(routes));

// Listen on page load:
window.addEventListener('load', Router.routeFn(routes));
