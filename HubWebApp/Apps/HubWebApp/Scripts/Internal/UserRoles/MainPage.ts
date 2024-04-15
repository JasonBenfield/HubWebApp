import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { UserRoleQueryPanel } from './UserRoleQueryPanel';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly userRoleQueryPanel: UserRoleQueryPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, view.mainMenuPanel)
        );
        this.userRoleQueryPanel = this.panels.add(
            new UserRoleQueryPanel(this.hubClient, view.userRoleQueryPanel)
        );
        this.userRoleQueryPanel.refresh();
        this.activateUserRoleQueryPanel();
    }

    private async activateUserRoleQueryPanel() {
        this.panels.activate(this.userRoleQueryPanel);
        const result = await this.userRoleQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateUserRoleQueryPanel();
        }
    }
}
new MainPage(new MainPageView());