import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Apis } from '../Apis';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { UserPanel } from './UserPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly userPanel: UserPanel;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(hubApi, this.view.mainMenuPanel)
        );
        this.userPanel = this.panels.add(
            new UserPanel(hubApi, this.view.userPanel)
        );
        this.userPanel.refresh();
        this.activateUserPanel();
    }

    private async activateUserPanel() {
        this.panels.activate(this.userPanel);
        const result = await this.userPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateUserPanel();
        }
    }
}
new MainPage();