import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { LogEntryQueryPanelView } from './LogEntryQueryPanelView';

export class MainPageView extends HubPageView {
    readonly logEntryQueryPanel: LogEntryQueryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.logEntryQueryPanel = this.addView(LogEntryQueryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}