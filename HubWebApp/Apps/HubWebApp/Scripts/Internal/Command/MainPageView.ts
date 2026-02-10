import { HubPageView } from '../HubPageView';
import { MainMenuPanelView } from '../MainMenuPanelView';
import { CommandPanelView } from './CommandPanelView';

export class MainPageView extends HubPageView {
    readonly mainMenuPanel: MainMenuPanelView;
    readonly commandPanel: CommandPanelView;

    constructor() {
        super();
        this.mainMenuPanel = this.addView(MainMenuPanelView);
        this.commandPanel = this.addView(CommandPanelView);
    }
}