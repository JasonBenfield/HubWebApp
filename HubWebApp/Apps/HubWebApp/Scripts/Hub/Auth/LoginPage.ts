import { AppApiFactory } from '@jasonbenfield/sharedwebapp/Api/AppApiFactory';
import { ModalErrorComponent } from '@jasonbenfield/sharedwebapp/Error/ModalErrorComponent';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { HubAppApi } from '../Api/HubAppApi';
import { LoginComponent } from "./LoginComponent";
import { LoginPageView } from "./LoginPageView";

class LoginPage {
    constructor(page: PageFrameView) {
        let view = new LoginPageView(page);
        let apiFactory = new AppApiFactory(new ModalErrorComponent(page.modalError));
        let hubApi = apiFactory.api(HubAppApi);
        new LoginComponent(hubApi, view.loginComponent);
    }
}
new LoginPage(new Startup().build());