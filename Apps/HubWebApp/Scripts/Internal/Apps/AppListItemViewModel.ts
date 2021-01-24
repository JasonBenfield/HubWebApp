import * as ko from 'knockout';
import * as template from './AppListItem.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';

export class AppListItemViewModel extends ComponentViewModel {
    constructor(public readonly source: IAppModel) {
        super(new ComponentTemplate('app-list-item', template));
    }

    readonly appName = ko.observable('');
    readonly title = ko.observable('');
    readonly type = ko.observable('');
    readonly url = ko.observable('');
}