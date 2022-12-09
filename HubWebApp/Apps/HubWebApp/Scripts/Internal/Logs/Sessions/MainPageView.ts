import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { SessionQueryPanelView } from './SessionQueryPanelView';

export class MainPageView extends HubPageView {
    readonly sessionQueryPanel: SessionQueryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.sessionQueryPanel = this.addView(SessionQueryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}