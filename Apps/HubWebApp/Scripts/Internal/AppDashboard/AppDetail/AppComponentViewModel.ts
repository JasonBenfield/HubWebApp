import * as ko from 'knockout';
import { AlertViewModel } from 'XtiShared/Alert';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import * as template from './AppComponent.html';

export class AppComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('app-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly appName = ko.observable('');
    readonly title = ko.observable('');
    readonly appType = ko.observable('');
}