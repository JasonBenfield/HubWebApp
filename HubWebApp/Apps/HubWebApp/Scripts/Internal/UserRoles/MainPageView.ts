import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { UserRoleQueryPanelView } from './UserRoleQueryPanelView';

export class MainPageView extends HubPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly userRoleQueryPanel: UserRoleQueryPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.userRoleQueryPanel = this.addView(UserRoleQueryPanelView);
    }
}