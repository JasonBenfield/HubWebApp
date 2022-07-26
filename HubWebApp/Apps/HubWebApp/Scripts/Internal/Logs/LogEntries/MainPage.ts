import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Panel/SingleActivePanel';
import { Apis } from '../../Apis';
import { MainMenuPanel } from '../../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { LogEntryQueryPanel } from './LogEntryQueryPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly logEntryQueryPanel: LogEntryQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.logEntryQueryPanel = this.panels.add(
            new LogEntryQueryPanel(this.view.logEntryQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(hubApi, this.view.mainMenuPanel)
        );
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