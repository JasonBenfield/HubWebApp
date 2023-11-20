import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { AddUserGroupPanel } from './AddUserGroupPanel';
import { MainPageView } from './MainPageView';
import { UserGroupsPanel } from './UserGroupsPanel';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly userGroupsPanel: UserGroupsPanel;
    private readonly addUserGroupPanel: AddUserGroupPanel;
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.userGroupsPanel = this.panels.add(
            new UserGroupsPanel(this.hubClient, this.view.userGroupsPanel)
        );
        this.addUserGroupPanel = this.panels.add(
            new AddUserGroupPanel(this.hubClient, this.view.addUserGroupPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        this.userGroupsPanel.refresh();
        this.activateUserGroupsPanel();
    }

    private async activateUserGroupsPanel() {
        this.panels.activate(this.userGroupsPanel);
        const result = await this.userGroupsPanel.start();
        if (result.addRequested) {
            this.activateAddUserGroupPanel();
        }
        else if (result.mainMenuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateAddUserGroupPanel() {
        this.panels.activate(this.addUserGroupPanel);
        const result = await this.addUserGroupPanel.start();
        if (result.done) {
            if (result.done.saved) {
                this.userGroupsPanel.refresh();
            }
            this.activateUserGroupsPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateUserGroupsPanel();
        }
    }
}
new MainPage();