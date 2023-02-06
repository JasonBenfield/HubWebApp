import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { InstallationPanelView } from './InstallationPanelView';

export class MainPageView extends HubPageView {
    readonly installationPanel: InstallationPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.installationPanel = this.addView(InstallationPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}