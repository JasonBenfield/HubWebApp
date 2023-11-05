import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { RequestPanel } from './RequestPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly requestPanel: RequestPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.requestPanel = this.panels.add(
            new RequestPanel(this.defaultClient, this.view.requestPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultClient, this.view.mainMenuPanel)
        );
        const requestIDText = Url.current().getQueryValue("RequestID");
        const requestID = requestIDText ? Number.parseInt(requestIDText) : 0;
        if (requestID) {
            this.requestPanel.setRequestID(requestID);
            this.requestPanel.refresh();
            this.activateRequestPanel();
        }
        else {
            this.defaultClient.Logs.AppRequests.open({ SessionID: null, InstallationID: null });
        }
    }

    private async activateRequestPanel() {
        this.panels.activate(this.requestPanel);
        const result = await this.requestPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateRequestPanel();
        }
    }
}
new MainPage();