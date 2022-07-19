import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { UserPanelView } from './User/UserPanelView';
import { UserEditPanelView } from './UserEdit/UserEditPanelView';

export class MainPageView extends BasicPageView {
    readonly userPanel: UserPanelView;
    readonly userEditPanel: UserEditPanelView;

    constructor() {
        super();
        this.userPanel = this.addView(UserPanelView);
        this.userEditPanel = this.addView(UserEditPanelView);
    }
}