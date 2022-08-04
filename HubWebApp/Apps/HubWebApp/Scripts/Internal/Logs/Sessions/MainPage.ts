import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Apis } from '../../Apis';
import { MainMenuPanel } from '../../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { SessionQueryPanel } from './SessionQueryPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly sessionQueryPanel: SessionQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.sessionQueryPanel = this.panels.add(
            new SessionQueryPanel(hubApi, this.view.sessionQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(hubApi, this.view.mainMenuPanel)
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