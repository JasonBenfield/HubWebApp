import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { RequestPanelView } from './RequestPanelView';

export class MainPageView extends HubPageView {
    readonly requestPanel: RequestPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.requestPanel = this.addView(RequestPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}