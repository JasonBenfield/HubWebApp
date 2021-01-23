import * as ko from 'knockout';
import * as template from './ResourceGroupComponent.html';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { AlertViewModel } from 'XtiShared/Alert';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class ResourceGroupComponentViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('resource-group-component', template));
    }

    readonly alert = new AlertViewModel();
    readonly groupName = ko.observable('');
    readonly isAnonymousAllowed = ko.observable(false);
}