import { Apis } from '../Apis';
import { AuthenticatorPage } from '../AuthenticatorPage';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage extends AuthenticatorPage {
    protected readonly view: MainPageView;

    constructor() {
        super(new MainPageView());
        const hubApi = new Apis(this.view.modalError).Hub();
        new LoginComponent(hubApi, this.view.loginComponent);
    }
}
new MainPage();