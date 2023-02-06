import { HubPageView } from '../../HubPageView';
import { MainMenuPanelView } from '../../MainMenuPanelView';
import { LogEntryPanelView } from './LogEntryPanelView';

export class MainPageView extends HubPageView {
    readonly logEntryPanel: LogEntryPanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.logEntryPanel = this.addView(LogEntryPanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}