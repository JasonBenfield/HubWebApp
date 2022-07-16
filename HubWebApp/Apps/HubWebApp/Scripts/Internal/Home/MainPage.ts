import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { MainPageView } from './MainPageView';

class MainPage extends BasicPage {
    constructor() {
        super(new MainPageView());
    }
}
new MainPage();