import { AuthenticatorPage } from '../AuthenticatorPage';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage extends AuthenticatorPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        new LoginComponent(this.hubClient, this.view.loginComponent);
    }
}
new MainPage();