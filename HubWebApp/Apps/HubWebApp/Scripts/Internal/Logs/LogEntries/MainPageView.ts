import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { LogEntryQueryPanelView } from './LogEntryQueryPanelView';

export class MainPageView extends BasicPageView {
    readonly logEntryQueryPanel: LogEntryQueryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.logEntryQueryPanel = this.addView(LogEntryQueryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}