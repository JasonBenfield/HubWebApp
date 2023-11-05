import { AppClients } from '../AppClients';
import { AuthenticatorPage } from '../AuthenticatorPage';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage extends AuthenticatorPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        const hubClient = new AppClients(this.view.modalError).Hub();
        new LoginComponent(hubClient, this.view.loginComponent);
    }
}
new MainPage();