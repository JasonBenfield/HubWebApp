import * as ko from 'knockout';

export class AppListItem {
    constructor(source: IAppModel) {
        this.key(source ? source.AppKey : '');
        this.title(source ? source.Title : '');
        this.type(source ? source.Type.DisplayText : '');
    }

    readonly key = ko.observable('');
    readonly title = ko.observable('');
    readonly type = ko.observable('');
}