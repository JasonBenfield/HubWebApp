import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/Api/XtiUrl';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { AddRolePanel } from './AddRolePanel';
import { AppUserDataPanel } from './AppUserDataPanel';
import { MainPageView } from './MainPageView';
import { SelectModCategoryPanel } from './SelectModCategoryPanel';
import { SelectModifierPanel } from './SelectModifierPanel';
import { UserRolesPanel } from './UserRolesPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;

    private readonly panels: SingleActivePanel;
    private readonly appUserDataPanel: AppUserDataPanel;
    private readonly selectModCategoryPanel: SelectModCategoryPanel;
    private readonly selectModifierPanel: SelectModifierPanel;
    private readonly userRolesPanel: UserRolesPanel;
    private readonly addRolePanel: AddRolePanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.appUserDataPanel = this.panels.add(
            new AppUserDataPanel(this.hubApi, this.view.appUserDataPanel)
        );
        this.selectModCategoryPanel = this.panels.add(
            new SelectModCategoryPanel(this.hubApi, this.view.selectModCategoryPanel)
        );
        this.selectModifierPanel = this.panels.add(
            new SelectModifierPanel(this.hubApi, this.view.selectModifierPanel)
        );
        this.userRolesPanel = this.panels.add(
            new UserRolesPanel(this.hubApi, this.view.userRolesPanel)
        );
        this.addRolePanel = this.panels.add(
            new AddRolePanel(this.hubApi, this.view.addRolePanel)
        );
        const userIDValue = Url.current().getQueryValue('UserID');
        if (XtiUrl.current().path.modifier && userIDValue) {
            const userID = Number(userIDValue);
            this.activateStartPanel(userID);
        }
        else {
            new WebPage(this.hubApi.UserGroups.Index.getUrl({})).open();
        }
    }

    private async activateStartPanel(userID: number) {
        this.panels.activate(this.appUserDataPanel);
        const result = await this.appUserDataPanel.start(userID);
        if (result.done) {
            this.userRolesPanel.setAppUserOptions(result.done.appUserOptions);
            this.userRolesPanel.setDefaultModifier();
            this.addRolePanel.setAppUserOptions(result.done.appUserOptions);
            this.addRolePanel.setDefaultModifier();
            this.activateUserRolesPanel();
        }
    }

    private async activateSelectModCategoryPanel() {
        this.panels.activate(this.selectModCategoryPanel);
        const result = await this.selectModCategoryPanel.start();
        if (result.defaultModSelected) {
            this.userRolesPanel.setDefaultModifier();
            this.addRolePanel.setDefaultModifier();
            this.activateUserRolesPanel();
        }
        else if (result.modCategorySelected) {
            this.selectModifierPanel.setModCategory(result.modCategorySelected.modCategory);
            this.userRolesPanel.setModCategory(result.modCategorySelected.modCategory);
            this.activateSelectModifierPanel();
        }
        else if (result.back) {
            this.activateUserRolesPanel();
        }
    }

    private async activateSelectModifierPanel() {
        this.panels.activate(this.selectModifierPanel);
        const result = await this.selectModifierPanel.start();
        if (result.modifierSelected) {
            this.userRolesPanel.setModifier(result.modifierSelected.modifier);
            this.addRolePanel.setModifier(result.modifierSelected.modifier);
            this.activateUserRolesPanel();
        }
        else if (result.back) {
            this.activateUserRolesPanel();
        }
    }

    private async activateUserRolesPanel() {
        this.panels.activate(this.userRolesPanel);
        const result = await this.userRolesPanel.start();
        if (result.addRequested) {
            this.activateAddRolePanel();
        }
        else if (result.modifierRequested) {
            this.activateSelectModCategoryPanel();
        }
    }

    private async activateAddRolePanel() {
        this.panels.activate(this.addRolePanel);
        const result = await this.addRolePanel.start();
        if (result.back) {
            this.activateUserRolesPanel();
        }
        else if (result.roleSelected) {
            this.activateUserRolesPanel();
        }
    }
}
new MainPage();