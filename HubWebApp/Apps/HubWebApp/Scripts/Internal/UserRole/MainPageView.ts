import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { UserRolePanelView } from './UserRolePanelView';

export class MainPageView extends HubPageView {
    readonly userRolePanelView: UserRolePanelView;
    readonly mainMenuPanelView: MainMenuPanelView;

    constructor() {
        super();
        this.userRolePanelView = this.addView(UserRolePanelView);
        this.mainMenuPanelView = this.addView(MainMenuPanelView);
    }
}