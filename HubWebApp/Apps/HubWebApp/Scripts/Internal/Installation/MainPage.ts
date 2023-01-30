import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { InstallationPanel } from './InstallationPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly installationPanel: InstallationPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.installationPanel = this.panels.add(
            new InstallationPanel(this.defaultApi, this.view.installationPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultApi, this.view.mainMenuPanel)
        );
        const installationIDText = Url.current().getQueryValue("InstallationID");
        const installationID = installationIDText ? Number.parseInt(installationIDText) : 0;
        if (installationID) {
            this.installationPanel.setInstallationID(installationID);
            this.installationPanel.refresh();
            this.activateInstallationPanel();
        }
        else {
            this.defaultApi.Installations.Index.open({ QueryType: null });
        }
    }

    private async activateInstallationPanel() {
        this.panels.activate(this.installationPanel);
        const result = await this.installationPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateInstallationPanel();
        }
    }
}
new MainPage();