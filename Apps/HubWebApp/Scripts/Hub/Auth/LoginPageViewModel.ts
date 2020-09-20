import * as template from './LoginPage.html';
import { LoginComponentViewModel } from './LoginComponentViewModel';
import { PageViewModel } from '../PageViewModel';

export class LoginPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly loginComponent = new LoginComponentViewModel();
}