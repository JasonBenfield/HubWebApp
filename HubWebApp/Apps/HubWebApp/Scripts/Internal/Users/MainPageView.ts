import { HubPageView } from '../HubPageView';
import { ChangePasswordPanelView } from './ChangePasswordPanelView';
import { EditUserGroupPanelView } from './EditUserGroupPanelView';
import { UserEditPanelView } from './UserEditPanelView';
import { UserPanelView } from './UserPanelView';

export class MainPageView extends HubPageView {
    readonly userPanel: UserPanelView;
    readonly userEditPanel: UserEditPanelView;
    readonly changePasswordPanel: ChangePasswordPanelView;
    readonly editUserGroupPanel: EditUserGroupPanelView;

    constructor() {
        super();
        this.userPanel = this.addView(UserPanelView);
        this.userEditPanel = this.addView(UserEditPanelView);
        this.changePasswordPanel = this.addView(ChangePasswordPanelView);
        this.editUserGroupPanel = this.addView(EditUserGroupPanelView);
    }
}