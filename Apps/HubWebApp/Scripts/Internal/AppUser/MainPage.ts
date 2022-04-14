import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/Api/XtiUrl';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../Apis';
import { AddRolePanel } from './AddRolePanel';
import { AppUserDataPanel } from './AppUserDataPanel';
import { MainPageView } from './MainPageView';
import { SelectModCategoryPanel } from './SelectModCategoryPanel';
import { SelectModifierPanel } from './SelectModifierPanel';
import { UserRolesPanel } from './UserRolesPanel';

class MainPage {
    private readonly view: MainPageView;
    private readonly hubApi: HubAppApi;

    private readonly panels: SingleActivePanel;
    private readonly appUserDataPanel: AppUserDataPanel;
    private readonly selectModCategoryPanel: SelectModCategoryPanel;
    private readonly selectModifierPanel: SelectModifierPanel;
    private readonly userRolesPanel: UserRolesPanel;
    private readonly addRolePanel: AddRolePanel;

    constructor(page: PageFrameView) {
        this.view = new MainPageView(page);
        this.hubApi = new Apis(page.modalError).Hub();
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
        let userIDValue = Url.current().getQueryValue('userID');
        if (XtiUrl.current.path.modifier && userIDValue) {
            let userID = Number(userIDValue);
            this.activateStartPanel(userID);
        }
        else {
            new WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }

    private async activateStartPanel(userID: number) {
        this.panels.activate(this.appUserDataPanel);
        let result = await this.appUserDataPanel.start(userID);
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
        let result = await this.selectModCategoryPanel.start();
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
        let result = await this.selectModifierPanel.start();
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
        let result = await this.userRolesPanel.start();
        if (result.addRequested) {
            this.activateAddRolePanel();
        }
        else if (result.modifierRequested) {
            this.activateSelectModCategoryPanel();
        }
    }

    private async activateAddRolePanel() {
        this.panels.activate(this.addRolePanel);
        let result = await this.addRolePanel.start();
        if (result.back) {
            this.activateUserRolesPanel();
        }
        else if (result.roleSelected) {
            this.activateUserRolesPanel();
        }
    }
}
new MainPage(new Startup().build());