import ko = require('knockout');
import { singleton } from 'tsyringe';
import { PageViewModel } from 'XtiShared/PageViewModel';
import * as template from './MainPage.html';
import { UserListPanelViewModel } from './UserListPanelViewModel';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly userListPanel = new UserListPanelViewModel();
}