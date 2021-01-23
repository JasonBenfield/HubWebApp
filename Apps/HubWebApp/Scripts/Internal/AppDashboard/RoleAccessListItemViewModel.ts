import * as ko from 'knockout';
import * as template from './RoleAccessListItem.html';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class RoleAccessListItemViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('role-access-list-item', template));
    }

    readonly roleName = ko.observable('');
    readonly isAllowed = ko.observable(false);
}