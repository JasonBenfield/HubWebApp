import { HubPageView } from '../HubPageView';
import { ChangePasswordPanelView } from './ChangePasswordPanelView';
import { UserEditPanelView } from './UserEditPanelView';
import { UserPanelView } from './UserPanelView';

export class MainPageView extends HubPageView {
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