import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { ChangePasswordPanel } from './ChangePasswordPanel';
import { MainPageView } from './MainPageView';
import { UserEditPanel } from './UserEditPanel';
import { UserPanel } from './UserPanel';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels: SingleActivePanel;
    private readonly userPanel: UserPanel;
    private readonly userEditPanel: UserEditPanel;
    private readonly changePasswordPanel: ChangePasswordPanel;

    constructor() {
        super(new MainPageView());
        this.panels = new SingleActivePanel();
        this.userPanel = this.panels.add(new UserPanel(this.defaultApi, this.view.userPanel));
        this.userEditPanel = this.panels.add(new UserEditPanel(this.defaultApi, this.view.userEditPanel));
        this.changePasswordPanel = this.panels.add(
            new ChangePasswordPanel(this.defaultApi, this.view.changePasswordPanel)
        );
        const userIDValue = Url.current().getQueryValue('UserID');
        const userID = userIDValue ? Number(userIDValue) : 0;
        if (userID) {
            this.userPanel.setUserID(userID);
            this.userEditPanel.setUserID(userID);
            this.changePasswordPanel.setUserID(userID);
            this.userPanel.refresh();
            this.activateUserPanel();
        }
        else {
            this.defaultApi.UserGroups.Index.open({}, '');
        }
    }

    private async activateUserPanel() {
        this.panels.activate(this.userPanel);
        const result = await this.userPanel.start();
        if (result.backRequested) {
            const url = this.defaultApi.UserGroups.Index.getModifierUrl('', {});
            new WebPage(url).open();
        }
        else if (result.editRequested) {
            this.activateUserEditPanel();
        }
        else if (result.changePasswordRequested) {
            this.activateChangePasswordPanel();
        }
    }

    private async activateUserEditPanel() {
        this.panels.activate(this.userEditPanel);
        this.userEditPanel.refresh();
        const result = await this.userEditPanel.start();
        if (result.canceled || result.saved) {
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
}
new MainPage();