import * as ko from 'knockout';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import * as template from './ModifierListItem.html';

export class ModifierListItemViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('modifier-list-item', template));
    }
    readonly modKey = ko.observable('');
    readonly displayText = ko.observable('');
}