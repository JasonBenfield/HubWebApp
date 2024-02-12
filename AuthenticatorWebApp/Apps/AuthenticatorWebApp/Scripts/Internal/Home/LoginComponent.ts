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
                const loginResult = result.value;
                new PostToLogin(this.hubClient).execute(cred, loginResult.AuthKey, loginResult.AuthID);
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
}