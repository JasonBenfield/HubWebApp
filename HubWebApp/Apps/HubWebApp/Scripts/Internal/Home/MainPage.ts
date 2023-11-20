import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        new MainMenuPanel(this.hubClient, this.view.mainMenuPanel);
    }
}
new MainPage();