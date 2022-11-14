﻿import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { ChangePasswordPanelView } from './ChangePasswordPanelView';
import { UserEditPanelView } from './UserEditPanelView';
import { UserPanelView } from './UserPanelView';

export class MainPageView extends BasicPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly userPanel: UserPanelView;
    readonly userEditPanel: UserEditPanelView;
    readonly changePasswordPanel: ChangePasswordPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.userPanel = this.addView(UserPanelView);
        this.userEditPanel = this.addView(UserEditPanelView);
        this.changePasswordPanel = this.addView(ChangePasswordPanelView);
    }
}