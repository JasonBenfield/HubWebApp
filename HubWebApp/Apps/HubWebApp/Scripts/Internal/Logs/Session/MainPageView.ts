import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { SessionPanelView } from './SessionPanelView';

export class MainPageView extends HubPageView {
    readonly sessionPanel: SessionPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.sessionPanel = this.addView(SessionPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}