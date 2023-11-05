import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { LogEntryQueryPanel } from './LogEntryQueryPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly logEntryQueryPanel: LogEntryQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.logEntryQueryPanel = this.panels.add(
            new LogEntryQueryPanel(this.defaultClient, this.view.logEntryQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultClient, this.view.mainMenuPanel)
        );
        this.logEntryQueryPanel.refresh();
        this.activateLogEntryQueryPanel();
    }

    private async activateLogEntryQueryPanel() {
        this.panels.activate(this.logEntryQueryPanel);
        const result = await this.logEntryQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateLogEntryQueryPanel();
        }
    }
}
new MainPage();