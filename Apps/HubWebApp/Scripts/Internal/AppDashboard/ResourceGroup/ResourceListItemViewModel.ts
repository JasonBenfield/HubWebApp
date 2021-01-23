import * as ko from 'knockout';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import * as template from './ResourceListItem.html';

export class ResourceListItemViewModel extends ComponentViewModel {
    constructor(readonly source: IResourceModel) {
        super(new ComponentTemplate('resource-list-item', template));
    }

    readonly name = ko.observable('');
    readonly resultType = ko.observable('');
}