﻿import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { SessionPanel } from './SessionPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly sessionPanel: SessionPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.sessionPanel = this.panels.add(
            new SessionPanel(this.hubClient, this.view.sessionPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        const sessionID = Url.current().query.getNumberValue("SessionID");
        if (sessionID) {
            this.sessionPanel.setSessionID(sessionID);
            this.sessionPanel.refresh();
            this.activateSessionPanel();
        }
        else {
            this.hubClient.Logs.Sessions.open({});
        }
    }

    private async activateSessionPanel() {
        this.panels.activate(this.sessionPanel);
        const result = await this.sessionPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateSessionPanel();
        }
    }
}
new MainPage();