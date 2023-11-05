import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { LogEntryPanel } from './LogEntryPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly logEntryPanel: LogEntryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.logEntryPanel = this.panels.add(
            new LogEntryPanel(this.defaultClient, this.view.logEntryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultClient, this.view.mainMenuPanel)
        );
        const logEntryIDText = Url.current().getQueryValue("LogEntryID");
        const logEntryID = logEntryIDText ? Number.parseInt(logEntryIDText) : 0;
        if (logEntryID) {
            this.logEntryPanel.setLogEntryID(logEntryID);
            this.logEntryPanel.refresh();
            this.activateLogEntryPanel();
        }
        else {
            this.defaultClient.Logs.LogEntries.open({ RequestID: null, InstallationID: null });
        }
    }

    private async activateLogEntryPanel() {
        this.panels.activate(this.logEntryPanel);
        const result = await this.logEntryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateLogEntryPanel();
        }
    }
}
new MainPage();