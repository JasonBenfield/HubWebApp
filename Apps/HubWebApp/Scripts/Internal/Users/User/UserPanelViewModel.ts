import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import { createCommandOutlineButtonViewModel } from "XtiShared/Templates/CommandOutlineButtonTemplate";
import { SelectableListCardViewModel } from "../../ListCard/SelectableListCardViewModel";
import { UserComponentViewModel } from "./UserComponentViewModel";
import * as template from './UserPanel.html';

export class UserPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('user-panel', template));
    }

    readonly userComponent = new UserComponentViewModel();
    readonly appListCard = new SelectableListCardViewModel();
    readonly backCommand = createCommandOutlineButtonViewModel();
}