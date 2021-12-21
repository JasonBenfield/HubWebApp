import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Command/AsyncCommand";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { UrlBuilder } from '@jasonbenfield/sharedwebapp/UrlBuilder';
import { VerifyLoginForm } from "../Api/VerifyLoginForm";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { BlockViewModel } from "@jasonbenfield/sharedwebapp/Html/BlockViewModel";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { MessageAlert } from '@jasonbenfield/sharedwebapp/MessageAlert';
import { TextCss } from '@jasonbenfield/sharedwebapp/TextCss';
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { HubAppApi } from "../Api/HubAppApi";
import { LoginComponentView } from "./LoginComponentView";

export class LoginResult {
    constructor(public readonly token: string) {
    }
}

export class LoginComponent {
    public static readonly ResultKeys = {
        loginComplete: 'login-complete'
    };

    private readonly verifyLoginForm: VerifyLoginForm;
    private readonly loginCommand = new AsyncCommand(this.login.bind(this));
    private readonly alert: MessageAlert;

    constructor(
        private readonly authApi: HubAppApi,
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
            let result = await this.verifyLoginForm.save(this.authApi.Auth.VerifyLoginAction);
            if (result.succeeded()) {
                let cred = this.getCredentials();
                this.alert.info('Opening page...');
                this.postLogin(cred);
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

    private postLogin(cred: ILoginCredentials) {
        let form = <HTMLFormElement>document.createElement('form');
        form.action = this.authApi.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        let userNameInput = this.createInput('Credentials.UserName', cred.UserName, 'text');
        let passwordInput = this.createInput('Credentials.Password', cred.Password, 'password');
        let urlBuilder = UrlBuilder.current();
        let startUrlInput = this.createInput('StartUrl', urlBuilder.getQueryValue('startUrl'));
        let returnUrlInput = this.createInput('ReturnUrl', urlBuilder.getQueryValue('returnUrl'));
        form.append(
            userNameInput,
            passwordInput,
            startUrlInput,
            returnUrlInput
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