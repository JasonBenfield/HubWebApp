import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import { SelectableListCardViewModel } from "../../ListCard/SelectableListCardViewModel";
import * as template from './UserListPanel.html';

export class UserListPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('user-list-panel', template));
    }

    readonly userListCard = new SelectableListCardViewModel();
}