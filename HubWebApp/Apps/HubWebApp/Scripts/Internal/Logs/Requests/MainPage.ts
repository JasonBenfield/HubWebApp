import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../../HubPage';
import { MainMenuPanel } from '../../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { RequestQueryPanel } from './RequestQueryPanel';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly requestQueryPanel: RequestQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.requestQueryPanel = this.panels.add(
            new RequestQueryPanel(this.defaultClient, this.view.requestQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.defaultClient, this.view.mainMenuPanel)
        );
        this.requestQueryPanel.refresh();
        this.activateRequestQueryPanel();
    }

    private async activateRequestQueryPanel() {
        this.panels.activate(this.requestQueryPanel);
        const result = await this.requestQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateRequestQueryPanel();
        }
    }
}
new MainPage();