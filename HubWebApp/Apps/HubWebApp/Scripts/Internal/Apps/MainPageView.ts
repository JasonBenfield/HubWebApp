import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { AppListPanelView } from './AppListPanelView';

export class MainPageView extends BasicPageView {
    readonly appListPanel: AppListPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.appListPanel = this.addView(AppListPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}