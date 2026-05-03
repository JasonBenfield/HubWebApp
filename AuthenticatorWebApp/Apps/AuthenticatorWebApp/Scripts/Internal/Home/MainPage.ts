import { AuthenticatorPage } from '../AuthenticatorPage';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage extends AuthenticatorPage {
    constructor(protected readonly view: MainPageView) {
        super(view);
        new LoginComponent(this.hubClient, this.view.loginComponent);
    }
}
new MainPage(new MainPageView());