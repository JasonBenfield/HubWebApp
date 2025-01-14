import { WebPage } from '@jasonbenfield/sharedwebapp/Http/WebPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { ChangePasswordPanel } from './ChangePasswordPanel';
import { MainPageView } from './MainPageView';
import { UserEditPanel } from './UserEditPanel';
import { UserPanel } from './UserPanel';
import { UrlBuilder } from '@jasonbenfield/sharedwebapp/UrlBuilder';
import { EditUserGroupPanel } from './EditUserGroupPanel';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly userPanel: UserPanel;
    private readonly userEditPanel: UserEditPanel;
    private readonly changePasswordPanel: ChangePasswordPanel;
    private readonly editUserGroupPanel: EditUserGroupPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.userPanel = this.panels.add(new UserPanel(this.hubClient, this.view.userPanel));
        this.userEditPanel = this.panels.add(new UserEditPanel(this.hubClient, this.view.userEditPanel));
        this.changePasswordPanel = this.panels.add(
            new ChangePasswordPanel(this.hubClient, this.view.changePasswordPanel)
        );
        this.editUserGroupPanel = this.panels.add(
            new EditUserGroupPanel(this.hubClient, view.editUserGroupPanel)
        );
        const userID = Url.current().query.getNumberValue("UserID");
        if (userID) {
            this.userPanel.setUserID(userID);
            this.userEditPanel.setUserID(userID);
            this.changePasswordPanel.setUserID(userID);
            this.editUserGroupPanel.setUserID(userID);
            this.userPanel.refresh();
            this.activateUserPanel();
        }
        else {
            this.hubClient.UserGroups.Index.open({}, '');
        }
    }

    private async activateUserPanel() {
        this.panels.activate(this.userPanel);
        const result = await this.userPanel.start();
        if (result.backRequested) {
            const returnTo = Url.current().query.getValue("ReturnTo");
            let url: UrlBuilder;
            if (returnTo) {
                url = this.hubClient.UserGroups.UserQuery.getModifierUrl("", { UserGroupName: returnTo });
            }
            else {
                url = this.hubClient.UserGroups.Index.getModifierUrl("", {});
            }
            new WebPage(url).open();
        }
        else if (result.editRequested) {
            this.activateUserEditPanel();
        }
        else if (result.changePasswordRequested) {
            this.activateChangePasswordPanel();
        }
        else if (result.editUserGroupRequested) {
            this.editUserGroupPanel.refresh();
            this.activateEditUserGroupPanel();
        }
    }

    private async activateUserEditPanel() {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.refresh();
        const result = await this.userEditPanel.start();
        if (result.canceled) {
            this.activateUserPanel();
        }
        else if (result.saved) {
            this.userPanel.refresh();
            this.activateUserPanel();
        }
    }

    private async activateChangePasswordPanel() {
        this.panels.activate(this.changePasswordPanel);
        const result = await this.changePasswordPanel.start();
        if (result.done) {
            this.activateUserPanel();
        }
    }

    private async activateEditUserGroupPanel() {
        this.panels.activate(this.editUserGroupPanel);
        const result = await this.editUserGroupPanel.start();
        if (result.backRequested) {
            this.activateUserPanel();
        }
        else if (result.userGroupChanged) {
            const returnTo = Url.current().query.getValue("ReturnTo");
            this.hubClient.Users.Index.open(
                {
                    UserID: result.userGroupChanged.userID,
                    ReturnTo: returnTo
                },
                result.userGroupChanged.userGroup.publicKey.displayText
            );
        }
    }
}
new MainPage(new MainPageView());