import { BasicPageView } from '../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicPageView';
import { AppListPanelView } from './AppListPanelView';

export class MainPageView extends BasicPageView {
    readonly appListPanel: AppListPanelView;

    constructor() {
        super();
        this.appListPanel = this.addView(AppListPanelView);
    }
}