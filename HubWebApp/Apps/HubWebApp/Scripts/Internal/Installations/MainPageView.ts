import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { InstallationQueryPanelView } from './InstallationQueryPanelView';

export class MainPageView extends HubPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly installationQueryPanel: InstallationQueryPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.installationQueryPanel = this.addView(InstallationQueryPanelView);
    }
}