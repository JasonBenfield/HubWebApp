import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    constructor(protected readonly view: MainPageView) {
        super(view);
        new MainMenuPanel(this.hubClient, this.view.mainMenuPanel);
    }
}
new MainPage(new MainPageView());