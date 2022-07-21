import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { UserPanelView } from './UserPanelView';
import { UserEditPanelView } from './UserEditPanelView';
import { ChangePasswordPanelView } from './ChangePasswordPanelView';

export class MainPageView extends BasicPageView {
    readonly userPanel: UserPanelView;
    readonly userEditPanel: UserEditPanelView;
    readonly changePasswordPanel: ChangePasswordPanelView;

    constructor() {
        super();
        this.userPanel = this.addView(UserPanelView);
        this.userEditPanel = this.addView(UserEditPanelView);
        this.changePasswordPanel = this.addView(ChangePasswordPanelView);
    }
}