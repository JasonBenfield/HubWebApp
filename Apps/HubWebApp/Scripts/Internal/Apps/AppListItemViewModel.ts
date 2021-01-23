import * as ko from 'knockout';

export class AppListItemViewModel {
    readonly appName = ko.observable('');
    readonly title = ko.observable('');
    readonly type = ko.observable('');
    readonly url = ko.observable('');
}