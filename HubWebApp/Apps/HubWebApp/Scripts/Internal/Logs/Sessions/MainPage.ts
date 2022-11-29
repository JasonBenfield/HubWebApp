import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { SessionQueryPanel } from './SessionQueryPanel';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly sessionQueryPanel: SessionQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.sessionQueryPanel = this.panels.add(
            new SessionQueryPanel(this.defaultApi, this.view.sessionQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultApi, this.view.mainMenuPanel)
        );
        this.sessionQueryPanel.refresh();
        this.activateSessionQueryPanel();
    }

    private async activateSessionQueryPanel() {
        this.panels.activate(this.sessionQueryPanel);
        const result = await this.sessionQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateSessionQueryPanel();
        }
    }
}
new MainPage();