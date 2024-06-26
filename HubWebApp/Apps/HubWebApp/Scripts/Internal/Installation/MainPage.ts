﻿import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { InstallationPanel } from './InstallationPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly installationPanel: InstallationPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.installationPanel = this.panels.add(
            new InstallationPanel(this.hubClient, this.view.installationPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        const installationID = Url.current().query.getNumberValue("InstallationID");
        if (installationID) {
            this.installationPanel.setInstallationID(installationID);
            this.installationPanel.refresh();
            this.activateInstallationPanel();
        }
        else {
            this.hubClient.Installations.Index.open({ QueryType: null });
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
new MainPage(new MainPageView());