import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';
import { UserRolePanel } from './UserRolePanel';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly userRolePanel: UserRolePanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.userRolePanel = this.panels.add(
            new UserRolePanel(this.hubClient, view.userRolePanelView)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, view.mainMenuPanelView)
        );
        const userRoleID = Url.current().query.getNumberValue("UserRoleID");
        if (userRoleID) {
            this.userRolePanel.setUserRoleID(userRoleID);
            this.userRolePanel.refresh();
            this.activateLogEntryPanel();
        }
        else {
            this.hubClient.UserRoles.Index.open({ AppID: null });
        }
    }

    private async activateLogEntryPanel() {
        this.panels.activate(this.userRolePanel);
        const result = await this.userRolePanel.start();
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
new MainPage(new MainPageView());