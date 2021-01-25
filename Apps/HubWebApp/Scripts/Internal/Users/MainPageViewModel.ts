import { singleton } from 'tsyringe';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { PanelViewModel } from '../Panel/PanelViewModel';
import * as template from './MainPage.html';
import { UserPanelViewModel } from './User/UserPanelViewModel';
import { UserListPanelViewModel } from './UserList/UserListPanelViewModel';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly userListPanel = new PanelViewModel(new UserListPanelViewModel());
    readonly userPanel = new PanelViewModel(new UserPanelViewModel());
}