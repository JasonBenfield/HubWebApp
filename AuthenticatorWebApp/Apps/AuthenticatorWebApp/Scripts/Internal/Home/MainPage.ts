import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { Apis } from '../Apis';
import { LoginComponent } from './LoginComponent';
import { MainPageView } from './MainPageView';

class MainPage {
    constructor(page: PageFrameView) {
        let view = new MainPageView(page);
        let hubApi = new Apis(page.modalError).Hub();
        new LoginComponent(hubApi, view.loginComponent);
    }
}
new MainPage(new Startup().build());