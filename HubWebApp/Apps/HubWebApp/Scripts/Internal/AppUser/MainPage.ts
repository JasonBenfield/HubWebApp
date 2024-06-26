﻿import { WebPage } from '@jasonbenfield/sharedwebapp/Http/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/Http/XtiUrl';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Url } from '@jasonbenfield/sharedwebapp/Url';
import { HubPage } from '../HubPage';
import { AddRolePanel } from './AddRolePanel';
import { AppUserDataPanel } from './AppUserDataPanel';
import { MainPageView } from './MainPageView';
import { SelectModCategoryPanel } from './SelectModCategoryPanel';
import { SelectModifierPanel } from './SelectModifierPanel';
import { UserRolesPanel } from './UserRolesPanel';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly appUserDataPanel: AppUserDataPanel;
    private readonly selectModCategoryPanel: SelectModCategoryPanel;
    private readonly selectModifierPanel: SelectModifierPanel;
    private readonly userRolesPanel: UserRolesPanel;
    private readonly addRolePanel: AddRolePanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.appUserDataPanel = this.panels.add(
            new AppUserDataPanel(this.hubClient, this.view.appUserDataPanel)
        );
        this.selectModCategoryPanel = this.panels.add(
            new SelectModCategoryPanel(this.hubClient, this.view.selectModCategoryPanel)
        );
        this.selectModifierPanel = this.panels.add(
            new SelectModifierPanel(this.hubClient, this.view.selectModifierPanel)
        );
        this.userRolesPanel = this.panels.add(
            new UserRolesPanel(this.hubClient, this.view.userRolesPanel)
        );
        this.addRolePanel = this.panels.add(
            new AddRolePanel(this.hubClient, this.view.addRolePanel)
        );
        const url = Url.current();
        const appModKey = url.query.getValue('App');
        const userID = url.query.getNumberValue('UserID');
        if (XtiUrl.current().path.modifier && userID && appModKey) {
            this.hubClient.App.withModifier(appModKey);
            this.hubClient.ModCategory.withModifier(appModKey);
            this.appUserDataPanel.setUserID(userID);
            this.activateAppUserDataPanel();
        }
        else {
            new WebPage(this.hubClient.UserGroups.Index.getUrl({})).open();
        }
    }

    private async activateAppUserDataPanel() {
        this.panels.activate(this.appUserDataPanel);
        const result = await this.appUserDataPanel.start();
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
new MainPage(new MainPageView());