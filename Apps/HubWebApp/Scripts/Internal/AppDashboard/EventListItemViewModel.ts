import * as ko from 'knockout';
import * as template from './EventListItem.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';

export class EventListItemViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('event-list-item', template));
    }

    readonly timeOccurred = ko.observable('');
    readonly severity = ko.observable('');
    readonly caption = ko.observable('');
    readonly message = ko.observable('');
}