import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import * as ko from 'knockout';
import * as template from './UserListItem.html';

export class UserListItemViewModel extends ComponentViewModel {
    constructor(public readonly source: IAppUserModel) {
        super(new ComponentTemplate('user-list-item', template));
    }

    readonly userName = ko.observable('');
    readonly name = ko.observable('');
}