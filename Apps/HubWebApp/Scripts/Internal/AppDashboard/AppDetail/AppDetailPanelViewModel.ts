import * as template from './AppDetailPanel.html';
import { AppComponentViewModel } from './AppComponentViewModel';
import { CurrentVersionComponentViewModel } from './CurrentVersionComponentViewModel';
import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { createCommandOutlineButtonViewModel } from 'XtiShared/Templates/CommandOutlineButtonTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ListCardViewModel } from '../../ListCard/ListCardViewModel';
import { SelectableListCardViewModel } from '../../ListCard/SelectableListCardViewModel';

export class AppDetailPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('app-detail-panel', template));
    }

    readonly app = new AppComponentViewModel();
    readonly currentVersion = new CurrentVersionComponentViewModel();
    readonly resourceGroupListCard = new SelectableListCardViewModel();
    readonly modifierCategoryListCard = new SelectableListCardViewModel();
    readonly mostRecentRequestListCard = new ListCardViewModel();
    readonly mostRecentErrorEventListCard = new ListCardViewModel();
    readonly backCommand = createCommandOutlineButtonViewModel();
}