import './css/site.css';
import 'bootstrap';
import * as ko from 'knockout';

import home from './components/home-page/home-page';
import counterExample from './components/counter-example/counter-example';
import fetchData from './components/fetch-data/fetch-data';
import about from './components/about/about';

ko.components.register('home-page', home);
ko.components.register('counter-example', counterExample);
ko.components.register('fetch-data', fetchData);
ko.components.register('about', about);

ko.applyBindings({});