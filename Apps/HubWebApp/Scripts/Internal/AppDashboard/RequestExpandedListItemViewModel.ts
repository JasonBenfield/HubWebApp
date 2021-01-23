import * as ko from 'knockout';
import * as template from './RequestExpandedListItem.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';

export class RequestExpandedListItemViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('request-expanded-list-item', template));
    }

    readonly groupName = ko.observable('');
    readonly actionName = ko.observable('');
    readonly userName = ko.observable('');
    readonly timeStarted = ko.observable('');
}