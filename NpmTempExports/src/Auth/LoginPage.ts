import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { Apis } from '../Apis';
import { LoginComponent } from "./LoginComponent";
import { LoginPageView } from "./LoginPageView";

class LoginPage {
    constructor(page: PageFrameView) {
        let view = new LoginPageView(page);
        let hubApi = new Apis(page.modalError).hub();
        new LoginComponent(hubApi, view.loginComponent);
    }
}
new LoginPage(new Startup().build());