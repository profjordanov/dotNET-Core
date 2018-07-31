import './css/site.css';
import 'bootstrap';
import * as ko from 'knockout';

ko.applyBindings({
    SimpleText: ko.observable<string>(
        (document.getElementById('SimpleText') as HTMLInputElement).value)
});