import * as ko from 'knockout';
import * as template from './ResourceGroupListItem.html';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';

export class ResourceGroupListItemViewModel extends ComponentViewModel {
    constructor(public readonly source: IResourceGroupModel) {
        super(new ComponentTemplate('resource-group-list-item', template));
    }
    readonly name = ko.observable('');
}