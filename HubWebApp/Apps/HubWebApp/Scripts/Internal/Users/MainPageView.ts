import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { UserPanelView } from './User/UserPanelView';
import { UserEditPanelView } from './UserEdit/UserEditPanelView';
import { UserListPanelView } from './UserList/UserListPanelView';

export class MainPageView extends BasicPageView {
    readonly userListPanel: UserListPanelView;
    readonly userPanel: UserPanelView;
    readonly userEditPanel: UserEditPanelView;

    constructor() {
        super();
        this.userListPanel = this.addView(UserListPanelView);
        this.userPanel = this.addView(UserPanelView);
        this.userEditPanel = this.addView(UserEditPanelView);
    }
}