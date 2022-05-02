import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Command/AsyncCommand";
import { MessageAlert } from '@jasonbenfield/sharedwebapp/MessageAlert';
import { UrlBuilder } from '@jasonbenfield/sharedwebapp/UrlBuilder';
import { HubAppApi } from "@hub/Api/HubAppApi";
import { PostToLogin } from "@hub/PostToLogin";
import { VerifyLoginForm } from "@hub/Api/VerifyLoginForm";
import { LoginComponentView } from "./LoginComponentView";

export class LoginResult {
    constructor(public readonly token: string) {
    }
}

export class LoginComponent {
    private readonly verifyLoginForm: VerifyLoginForm;
    private readonly loginCommand = new AsyncCommand(this.login.bind(this));
    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: LoginComponentView
    ) {
        this.verifyLoginForm = new VerifyLoginForm(this.view.verifyLoginForm);
        this.alert = new MessageAlert(this.view.alert);
        this.view.formSubmitted.register(this.onSubmit.bind(this));
        this.loginCommand.add(this.view.loginButton);
    }

    private onSubmit() {
        return this.loginCommand.execute();
    }

    private async login() {
        this.alert.info('Verifying login...');
        try {
            let result = await this.verifyLoginForm.save(this.hubApi.Auth.VerifyLoginAction);
            if (result.succeeded()) {
                let cred = this.getCredentials();
                this.alert.info('Opening page...');
                new PostToLogin(this.hubApi).execute(cred, result.value);
                this.postLogin(cred, result.value);
            }
        }
        finally {
            this.alert.clear();
        }
    }

    private getCredentials() {
        return <ILoginCredentials>{
            UserName: this.verifyLoginForm.UserName.getValue(),
            Password: this.verifyLoginForm.Password.getValue()
        };
    }

    private postLogin(cred: ILoginCredentials, authKey: string) {
        let form = <HTMLFormElement>document.createElement('form');
        form.action = this.hubApi.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        let userNameInput = this.createInput('UserName', cred.UserName, 'text');
        let passwordInput = this.createInput('Password', cred.Password, 'password');
        let urlBuilder = UrlBuilder.current();
        let authKeyInput = this.createInput('AuthKey', authKey);
        let returnKeyInput = this.createInput('ReturnKey', urlBuilder.getQueryValue('returnKey'));
        form.append(
            userNameInput,
            passwordInput,
            authKeyInput,
            returnKeyInput
        );
        document.body.append(form);
        form.submit();
    }

    private createInput(name: string, value: string, type: string = 'hidden') {
        let input = <HTMLInputElement>document.createElement('input');
        input.type = type;
        input.name = name;
        input.value = value;
        return input;
    }
}