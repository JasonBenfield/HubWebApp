import * as ko from 'knockout';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { AlertViewModel } from 'XtiShared/Alert';
import * as template from './ResourceComponent.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class ResourceComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('resource-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly resourceName = ko.observable('');
    readonly isAnonymousAllowed = ko.observable(false);
    readonly resultType = ko.observable('');
}