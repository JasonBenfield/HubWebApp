import { ComponentTemplate } from 'XtiShared/ComponentTemplate';
import { createCommandOutlineButtonViewModel } from 'XtiShared/Templates/CommandOutlineButtonTemplate';
import { ComponentViewModel } from 'XtiShared/ComponentViewModel';
import { ResourceComponentViewModel } from './ResourceComponentViewModel';
import * as template from './ResourcePanel.html';
import { ListCardViewModel } from '../ListCardViewModel';

export class ResourcePanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('resource-panel', template));
    }

    readonly resourceComponent = new ResourceComponentViewModel();
    readonly resourceAccessCard = new ListCardViewModel();
    readonly mostRecentRequestListCard = new ListCardViewModel();
    readonly mostRecentErrorEventListCard = new ListCardViewModel();
    readonly backCommand = createCommandOutlineButtonViewModel();
}