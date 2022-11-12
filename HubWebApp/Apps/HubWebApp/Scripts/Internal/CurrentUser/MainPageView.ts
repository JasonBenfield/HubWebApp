import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { UserPanelView } from './UserPanelView';

export class MainPageView extends BasicPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly userPanel: UserPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.userPanel = this.addView(UserPanelView);
    }
}