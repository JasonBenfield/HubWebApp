import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Url';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { MainPageView } from './MainPageView';
import { UserPanel } from './User/UserPanel';
import { UserEditPanel } from './UserEdit/UserEditPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly panels: SingleActivePanel;
    private readonly userPanel: UserPanel;
    private readonly userEditPanel: UserEditPanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.userPanel = this.panels.add(new UserPanel(this.hubApi, this.view.userPanel));
        this.userEditPanel = this.panels.add(new UserEditPanel(this.hubApi, this.view.userEditPanel));
        const userIDValue = Url.current().getQueryValue('UserID');
        const userID = userIDValue ? Number(userIDValue) : 0;
        if (userID) {
            this.activateUserPanel(userID);
        }
        else {
            this.hubApi.UserGroups.Index.open({});
        }
    }

    private async activateUserPanel(userID: number) {
        this.panels.activate(this.userPanel);
        this.userPanel.setUserID(userID);
        this.userPanel.refresh();
        const result = await this.userPanel.start();
        if (result.backRequested) {
            this.hubApi.UserGroups.Index.open({});
        }
        else if (result.editRequested) {
            this.activateUserEditPanel(userID);
        }
        else if (result.appSelected) {
            const url = this.hubApi.AppUser.Index.getModifierUrl(
                result.appSelected.app.PublicKey.DisplayText,
                { UserID: userID }
            );
            new WebPage(url).open();
        }
    }

    private async activateUserEditPanel(userID: number) {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.setUserID(userID);
        this.userEditPanel.refresh();
        const result = await this.userEditPanel.start();
        if (result.canceled || result.saved) {
            this.activateUserPanel(userID);
        }
    }
}
new MainPage();