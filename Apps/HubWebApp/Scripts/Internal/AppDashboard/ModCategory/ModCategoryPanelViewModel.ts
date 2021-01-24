import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { createCommandOutlineButtonViewModel } from 'XtiShared/Templates/CommandOutlineButtonTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import * as template from './ModCategoryPanel.html';
import { ModCategoryComponentViewModel } from './ModCategoryComponentViewModel';
import { SelectableListCardViewModel } from '../../ListCard/SelectableListCardViewModel';
import { ListCardViewModel } from '../../ListCard/ListCardViewModel';

export class ModCategoryPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('mod-category-panel', template));
    }

    readonly modCategoryComponent = new ModCategoryComponentViewModel();
    readonly modifierListCard = new ListCardViewModel();
    readonly resourceGroupListCard = new SelectableListCardViewModel();
    readonly backCommand = createCommandOutlineButtonViewModel();
}
