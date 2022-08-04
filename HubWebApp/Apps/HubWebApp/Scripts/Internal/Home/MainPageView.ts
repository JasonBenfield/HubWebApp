import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';

export class MainPageView extends BasicPageView {
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.mainMenuPanel.hideToolbar();
    }
}