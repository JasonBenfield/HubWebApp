import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from '@jasonbenfield/sharedwebapp/Components/MessageAlert';
import { UrlBuilder } from '@jasonbenfield/sharedwebapp/UrlBuilder';
import { HubAppClient } from "@hub/Http/HubAppClient";
import { PostToLogin } from "@hub/PostToLogin";
import { VerifyLoginForm } from "@hub/Http/VerifyLoginForm";
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
        private readonly hubClient: HubAppClient,
        view: LoginComponentView
    ) {
        this.verifyLoginForm = new VerifyLoginForm(view.verifyLoginForm);
        this.verifyLoginForm.handleSubmit(this.onSubmit.bind(this));
        this.alert = new MessageAlert(view.alert);
        this.loginCommand.add(view.loginButton);
    }

    private onSubmit() {
        return this.loginCommand.execute();
    }

    private async login() {
        this.alert.info('Verifying login...');
        try {
            const result = await this.verifyLoginForm.save(this.hubClient.Auth.VerifyLoginAction);
            if (result.succeeded()) {
                const cred = this.getCredentials();
                this.alert.info('Opening page...');
                new PostToLogin(this.hubClient).execute(cred, result.value);
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
        const form = <HTMLFormElement>document.createElement('form');
        form.action = this.hubClient.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        const userNameInput = this.createInput('UserName', cred.UserName, 'text');
        const passwordInput = this.createInput('Password', cred.Password, 'password');
        const urlBuilder = UrlBuilder.current();
        const authKeyInput = this.createInput('AuthKey', authKey);
        const returnKeyInput = this.createInput('ReturnKey', urlBuilder.query.getValue('returnKey'));
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
        const input = <HTMLInputElement>document.createElement('input');
        input.type = type;
        input.name = name;
        input.value = value;
        return input;
    }
}