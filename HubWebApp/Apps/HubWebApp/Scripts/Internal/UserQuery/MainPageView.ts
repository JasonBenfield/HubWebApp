import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { AddUserPanelView } from './AddUserPanelView';
import { UserQueryPanelView } from './UserQueryPanelView';

export class MainPageView extends HubPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly userQueryPanel: UserQueryPanelView;
    readonly addUserPanel: AddUserPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.userQueryPanel = this.addView(UserQueryPanelView);
        this.addUserPanel = this.addView(AddUserPanelView);
    }
}