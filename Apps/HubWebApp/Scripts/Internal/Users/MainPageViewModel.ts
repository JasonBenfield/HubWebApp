import { singleton } from 'tsyringe';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { PanelViewModel } from '../Panel/PanelViewModel';
import * as template from './MainPage.html';
import { UserPanelViewModel } from './User/UserPanelViewModel';
import { UserEditPanelViewModel } from './UserEdit/UserEditPanelViewModel';
import { UserListPanelViewModel } from './UserList/UserListPanelViewModel';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly userListPanel = new PanelViewModel(new UserListPanelViewModel());
    readonly userPanel = new PanelViewModel(new UserPanelViewModel());
    readonly userEditPanel = new PanelViewModel(new UserEditPanelViewModel());
}