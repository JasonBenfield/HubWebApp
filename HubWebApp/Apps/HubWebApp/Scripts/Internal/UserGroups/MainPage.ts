import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { MainPageView } from './MainPageView';
import { UserGroupsPanel } from './UserGroupsPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly userGroupsPanel: UserGroupsPanel;
    private readonly panels: SingleActivePanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.userGroupsPanel = this.panels.add(
            new UserGroupsPanel(this.hubApi, this.view.userGroupsPanel)
        );
        this.userGroupsPanel.refresh();
        this.activateUserGroupsPanel();
    }

    private async activateUserGroupsPanel() {
        this.panels.activate(this.userGroupsPanel);
        const result = await this.userGroupsPanel.start();
        if (result.addRequested) {

        }
    }
}
new MainPage();