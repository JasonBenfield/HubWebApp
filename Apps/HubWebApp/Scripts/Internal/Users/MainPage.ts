import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../Apis';
import { MainPageView } from './MainPageView';
import { UserPanel } from './User/UserPanel';
import { UserEditPanel } from './UserEdit/UserEditPanel';
import { UserListPanel } from './UserList/UserListPanel';

class MainPage {
    private readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly panels: SingleActivePanel;
    private readonly userListPanel: UserListPanel;
    private readonly userPanel: UserPanel;
    private readonly userEditPanel: UserEditPanel;

    constructor(page: PageFrameView) {
        this.view = new MainPageView(page);
        this.hubApi = new Apis(page.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.userListPanel = this.panels.add(new UserListPanel(this.hubApi, this.view.userListPanel));
        this.userPanel = this.panels.add(new UserPanel(this.hubApi, this.view.userPanel));
        this.userEditPanel = this.panels.add(new UserEditPanel(this.hubApi, this.view.userEditPanel));
        this.activateUserListPanel();
    }


    private async activateUserListPanel() {
        this.panels.activate(this.userListPanel);
        this.userListPanel.refresh();
        let result = await this.userListPanel.start();
        if (result.userSelected) {
            this.activateUserPanel(result.userSelected.user.ID);
        }
    }

    private async activateUserPanel(userID: number) {
        this.panels.activate(this.userPanel);
        this.userPanel.setUserID(userID);
        this.userPanel.refresh();
        let result = await this.userPanel.start();
        if (result.backRequested) {
            this.activateUserListPanel();
        }
        else if (result.editRequested) {
            this.activateUserEditPanel(userID);
        }
        else if (result.appSelected) {
            this.hubApi.UserInquiry.RedirectToAppUser.open({
                UserID: userID,
                AppID: result.appSelected.app.ID
            });
        }
    }

    private async activateUserEditPanel(userID: number) {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.setUserID(userID);
        this.userEditPanel.refresh();
        let result = await this.userEditPanel.start();
        if (result.canceled || result.saved) {
            this.activateUserPanel(userID);
        }
    }
}
new MainPage(new Startup().build());