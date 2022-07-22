import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { UserGroupsPanelView } from './UserGroupsPanelView';

export class MainPageView extends BasicPageView {
    readonly userGroupsPanel: UserGroupsPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.userGroupsPanel = this.addView(UserGroupsPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}