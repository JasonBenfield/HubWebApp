import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Apis } from '../../Apis';
import { MainMenuPanel } from '../../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { RequestQueryPanel } from './RequestQueryPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly requestQueryPanel: RequestQueryPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.requestQueryPanel = this.panels.add(
            new RequestQueryPanel(hubApi, this.view.requestQueryPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(hubApi, this.view.mainMenuPanel)
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