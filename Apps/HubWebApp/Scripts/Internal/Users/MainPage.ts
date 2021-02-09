import { Startup } from 'xtistart';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { UserListPanel } from './UserList/UserListPanel';
import { SingleActivePanel } from '../Panel/SingleActivePanel';
import { UserPanel } from './User/UserPanel';
import { UserEditPanel } from './UserEdit/UserEditPanel';
import { PageFrame } from 'XtiShared/PageFrame';
import { PaddingCss } from 'XtiShared/PaddingCss';

class MainPage {
    constructor(private readonly page: PageFrame) {
        this.page.content.setPadding(PaddingCss.top(3));
        this.activateUserListPanel();
    }

    private readonly hubApi = this.page.api(HubAppApi);
    private readonly panels = new SingleActivePanel();
    private readonly userListPanel = this.page.addContent(
        this.panels.add(new UserListPanel(this.hubApi))
    );
    private readonly userPanel = this.page.addContent(
        this.panels.add(new UserPanel(this.hubApi))
    );
    private readonly userEditPanel = this.page.addContent(
        this.panels.add(new UserEditPanel(this.hubApi))
    );

    private async activateUserListPanel() {
        this.panels.activate(this.userListPanel);
        this.userListPanel.content.refresh();
        let result = await this.userListPanel.content.start();
        if (result.key === UserListPanel.ResultKeys.userSelected) {
            let user: IAppUserModel = result.data;
            this.activateUserPanel(user.ID);
        }
    }

    private async activateUserPanel(userID: number) {
        this.panels.activate(this.userPanel);
        this.userPanel.content.setUserID(userID);
        this.userPanel.content.refresh();
        let result = await this.userPanel.content.start();
        if (result.key === UserPanel.ResultKeys.backRequested) {
            this.activateUserListPanel();
        }
        else if (result.key === UserPanel.ResultKeys.editRequested) {
            this.activateUserEditPanel(userID);
        }
        else if (result.key === UserPanel.ResultKeys.appSelected) {
            let app: IAppModel = result.data;
            this.hubApi.UserInquiry.RedirectToAppUser.open({ UserID: userID, AppID: app.ID });
        }
    }

    private async activateUserEditPanel(userID: number) {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.content.setUserID(userID);
        this.userEditPanel.content.refresh();
        let result = await this.userEditPanel.content.start();
        if (result.key === UserEditPanel.ResultKeys.canceled ||
            result.key === UserEditPanel.ResultKeys.saved) {
            this.activateUserPanel(userID);
        }
    }
}
new MainPage(new Startup().build());