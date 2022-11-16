import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { AddUserGroupPanelView } from './AddUserGroupPanelView';
import { UserGroupsPanelView } from './UserGroupsPanelView';

export class MainPageView extends HubPageView {
    readonly userGroupsPanel: UserGroupsPanelView;
    readonly addUserGroupPanel: AddUserGroupPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.userGroupsPanel = this.addView(UserGroupsPanelView);
        this.addUserGroupPanel = this.addView(AddUserGroupPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}