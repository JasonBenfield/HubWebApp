import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { AddUserPanel } from './AddUserPanel';
import { MainPageView } from './MainPageView';
import { UserQueryPanel } from './UserQueryPanel';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;
    private readonly userQueryPanel: UserQueryPanel;
    private readonly addUserPanel: AddUserPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        this.userQueryPanel = this.panels.add(
            new UserQueryPanel(this.hubClient, this.view.userQueryPanel)
        );
        this.addUserPanel = this.panels.add(
            new AddUserPanel(this.hubClient, this.view.addUserPanel)
        );
        const userGroupName = Url.current().getQueryValue('UserGroupName') || '';
        this.hubClient.Users.withModifier(userGroupName);
        this.userQueryPanel.setUserGroupName(userGroupName);
        this.userQueryPanel.refresh();
        this.activateUserQueryPanel();
    }

    private async activateUserQueryPanel() {
        this.panels.activate(this.userQueryPanel);
        const result = await this.userQueryPanel.start();
        if (result.menuRequested) {
            this.activateMainMenuPanel();
        }
        else if (result.addRequested) {
            this.activateAddUserPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateUserQueryPanel();
        }
    }

    private async activateAddUserPanel() {
        this.panels.activate(this.addUserPanel);
        const result = await this.addUserPanel.start();
        if (result.done) {
            if (result.done.saved) {
                this.userQueryPanel.refresh();
            }
            this.activateUserQueryPanel();
        }
    }
}
new MainPage();