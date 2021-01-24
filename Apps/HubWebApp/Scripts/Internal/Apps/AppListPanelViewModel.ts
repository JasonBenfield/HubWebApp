import { ComponentViewModel } from "XtiShared/ComponentViewModel";
import { ComponentTemplate } from "XtiShared/ComponentTemplate";
import { SelectableListCardViewModel } from "../ListCard/SelectableListCardViewModel";
import * as template from './AppListPanel.html';

export class AppListPanelViewModel extends ComponentViewModel {
    constructor() {
        super(new ComponentTemplate('app-list-panel', template));
    }

    readonly appListCard = new SelectableListCardViewModel();
}