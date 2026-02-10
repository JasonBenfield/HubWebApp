import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';

export class MainPageView extends HubPageView {
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.mainMenuPanel.hideToolbar();
    }
}