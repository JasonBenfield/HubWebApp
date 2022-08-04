import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { RequestQueryPanelView } from './RequestQueryPanelView';

export class MainPageView extends BasicPageView {
    readonly requestQueryPanel: RequestQueryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.requestQueryPanel = this.addView(RequestQueryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}