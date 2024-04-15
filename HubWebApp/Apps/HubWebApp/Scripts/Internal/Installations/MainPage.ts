import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { InstallationQueryPanel } from './InstallationQueryPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    private readonly panels = new SingleActivePanel();
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly installationQueryPanel: InstallationQueryPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.mainMenuPanel = this.panels.add(new MainMenuPanel(this.hubClient, this.view.mainMenuPanel));
        this.installationQueryPanel = this.panels.add(new InstallationQueryPanel(this.hubClient, this.view.installationQueryPanel));
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
new MainPage(new MainPageView());