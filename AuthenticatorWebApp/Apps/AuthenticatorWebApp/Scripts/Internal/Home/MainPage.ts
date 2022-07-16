import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { Apis } from '../Apis';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        new LoginComponent(hubApi, this.view.loginComponent);
    }
}
new MainPage();