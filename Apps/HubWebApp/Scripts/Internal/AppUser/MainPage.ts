import { Startup } from 'xtistart';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { XtiUrl } from 'XtiShared/XtiUrl';
import { WebPage } from 'XtiShared/WebPage';
import { SingleActivePanel } from '../Panel/SingleActivePanel';
import { Url } from 'XtiShared/Url';
import { AppUserPanel } from './AppUser/AppUserPanel';
import { PageFrame } from 'XtiShared/PageFrame';
import { PaddingCss } from 'XtiShared/PaddingCss';
import { UserRolePanel } from './UserRoles/UserRolePanel';

class MainPage {
    constructor(private readonly page: PageFrame) {
        this.page.content.setPadding(PaddingCss.top(3));
        let userID = Url.current().getQueryValue('userID');
        if (XtiUrl.current.path.modifier && userID) {
            this.activateAppUserPanel(Number(userID));
        }
        else {
            new WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }

    private readonly hubApi = this.page.api(HubAppApi);

    private readonly panels = new SingleActivePanel();
    private readonly appUserPanel = this.page.addContent(
        this.panels.add(new AppUserPanel(this.hubApi))
    );
    private readonly userRolePanel = this.page.addContent(
        this.panels.add(new UserRolePanel(this.hubApi))
    );

    private async activateAppUserPanel(userID: number) {
        this.panels.activate(this.appUserPanel);
        this.appUserPanel.content.setUserID(userID);
        this.appUserPanel.content.refresh();
        let result = await this.appUserPanel.content.start();
        if (result.key === AppUserPanel.ResultKeys.backRequested) {
            this.hubApi.Users.Index.open({});
        }
        else if (result.key === AppUserPanel.ResultKeys.editUserRolesRequested) {
            this.activateUserRolePanel(userID);
        }
    }

    private async activateUserRolePanel(userID: number) {
        this.panels.activate(this.userRolePanel);
        this.userRolePanel.content.setUserID(userID);
        this.userRolePanel.content.refresh();
        let result = await this.userRolePanel.content.start();
        if (result.key === AppUserPanel.ResultKeys.backRequested) {
            this.activateAppUserPanel(userID);
        }
    }
}
new MainPage(new Startup().build());