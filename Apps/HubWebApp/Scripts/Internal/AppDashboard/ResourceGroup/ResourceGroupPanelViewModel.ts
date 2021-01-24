import * as template from './ResourceGroupPanel.html';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { SelectableListCardViewModel } from '../../ListCard/SelectableListCardViewModel';
import { ResourceGroupComponentViewModel } from './ResourceGroupComponentViewModel';
import { createCommandOutlineButtonViewModel } from 'XtiShared/Templates/CommandOutlineButtonTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ModCategoryComponentViewModel } from './ModCategoryComponentViewModel';
import { ListCardViewModel } from '../../ListCard/ListCardViewModel';

export class ResourceGroupPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('resource-group-panel', template));
    }

    readonly resourceGroupComponent = new ResourceGroupComponentViewModel();
    readonly modCategoryComponent = new ModCategoryComponentViewModel();
    readonly roleAccessCard = new ListCardViewModel();
    readonly resourceListCard = new SelectableListCardViewModel();
    readonly mostRecentRequestListCard = new ListCardViewModel();
    readonly mostRecentErrorEventListCard = new ListCardViewModel();
    readonly backCommand = createCommandOutlineButtonViewModel();
}