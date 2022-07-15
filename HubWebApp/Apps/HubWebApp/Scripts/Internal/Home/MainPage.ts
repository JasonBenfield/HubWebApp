import { BasicPage } from '../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicPage';
import { MainPageView } from './MainPageView';

class MainPage extends BasicPage {
    constructor() {
        super(new MainPageView());
    }
}
new MainPage();