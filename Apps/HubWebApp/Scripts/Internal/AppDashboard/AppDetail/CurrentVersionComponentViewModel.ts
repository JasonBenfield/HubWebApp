import * as ko from 'knockout';
import { AlertViewModel } from 'XtiShared/Alert';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import * as template from './CurrentVersionComponent.html';

export class CurrentVersionComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('current-version-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly versionKey = ko.observable('');
    readonly version = ko.observable('');
}