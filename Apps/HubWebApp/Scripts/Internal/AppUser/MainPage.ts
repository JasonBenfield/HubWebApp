import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { WebPage } from '@jasonbenfield/sharedwebapp/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/XtiUrl';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../../Hub/Apis';
import { AppUserPanel } from './AppUser/AppUserPanel';
import { MainPageView } from './MainPageView';
import { UserRolePanel } from './UserRoles/UserRolePanel';

class MainPage {
    private readonly view: MainPageView;
    private readonly hubApi: HubAppApi;

    private readonly panels = new SingleActivePanel();
    private readonly appUserPanel: AppUserPanel;
    private readonly userRolePanel: UserRolePanel;


    constructor(page: PageFrameView) {
        this.view = new MainPageView(page);
        this.hubApi = new Apis(page.modalError).hub();
        this.appUserPanel = this.panels.add(new AppUserPanel(this.hubApi, this.view.appUserPanel));
        this.userRolePanel = this.panels.add(new UserRolePanel(this.hubApi, this.view.userRolePanel));
        let userID = Url.current().getQueryValue('userID');
        if (XtiUrl.current.path.modifier && userID) {
            this.activateAppUserPanel(Number(userID));
        }
        else {
            new WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }

    private async activateAppUserPanel(userID: number) {
        this.panels.activate(this.appUserPanel);
        this.appUserPanel.setUserID(userID);
        this.appUserPanel.refresh();
        let result = await this.appUserPanel.start();
        if (result.backRequested) {
            this.hubApi.Users.Index.open({});
        }
        else if (result.editUserRolesRequested) {
            this.activateUserRolePanel(userID);
        }
        else if (result.editUserModCategoryRequested) {
        }
    }

    private async activateUserRolePanel(userID: number) {
        this.panels.activate(this.userRolePanel);
        this.userRolePanel.setUserID(userID);
        this.userRolePanel.refresh();
        let result = await this.userRolePanel.start();
        if (result.backRequested) {
            this.activateAppUserPanel(userID);
        }
    }
}
new MainPage(new Startup().build());