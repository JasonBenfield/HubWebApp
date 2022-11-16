import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { RequestQueryPanelView } from './RequestQueryPanelView';

export class MainPageView extends HubPageView {
    readonly requestQueryPanel: RequestQueryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.requestQueryPanel = this.addView(RequestQueryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}