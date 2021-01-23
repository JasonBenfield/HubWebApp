import * as ko from 'knockout';
import * as template from './ModifierCategoryListItem.html';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';

export class ModifierCategoryListItemViewModel extends ComponentViewModel {
    constructor(public readonly source: IModifierCategoryModel) {
        super(new ComponentTemplate('mod-category-list-item', template));
    }

    readonly name = ko.observable('');
}