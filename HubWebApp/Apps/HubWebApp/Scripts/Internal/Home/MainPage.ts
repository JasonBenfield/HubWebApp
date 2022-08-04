import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { Apis } from '../Apis';
import { MainMenuPanel } from '../MainMenuPanel';
import { MainPageView } from './MainPageView';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        new MainMenuPanel(hubApi, this.view.mainMenuPanel);
    }
}
new MainPage();