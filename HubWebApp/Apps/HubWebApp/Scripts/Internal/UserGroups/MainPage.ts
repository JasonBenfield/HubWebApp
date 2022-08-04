import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { MainMenuPanel } from '../MainMenuPanel';
import { AddUserGroupPanel } from './AddUserGroupPanel';
import { MainPageView } from './MainPageView';
import { UserGroupsPanel } from './UserGroupsPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly userGroupsPanel: UserGroupsPanel;
    private readonly addUserGroupPanel: AddUserGroupPanel;
    private readonly panels: SingleActivePanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.userGroupsPanel = this.panels.add(
            new UserGroupsPanel(this.hubApi, this.view.userGroupsPanel)
        );
        this.addUserGroupPanel = this.panels.add(
            new AddUserGroupPanel(this.hubApi, this.view.addUserGroupPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubApi, this.view.mainMenuPanel)
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