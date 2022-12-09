import { HubPage } from '../HubPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Apis } from '../Apis';
import { MainMenuPanel } from '../MainMenuPanel';
import { InstallationQueryPanel } from './InstallationQueryPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels = new SingleActivePanel();
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly installationQueryPanel: InstallationQueryPanel;

    constructor() {
        super(new MainPageView());
        this.mainMenuPanel = this.panels.add(new MainMenuPanel(this.defaultApi, this.view.mainMenuPanel));
        this.installationQueryPanel = this.panels.add(new InstallationQueryPanel(this.defaultApi, this.view.installationQueryPanel));
        this.installationQueryPanel.refresh();
        this.activateInstallationQueryPanel();
    }

    private async activateInstallationQueryPanel() {
        this.panels.activate(this.installationQueryPanel);
        const result = await this.installationQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateInstallationQueryPanel();
        }
    }
}
new MainPage();