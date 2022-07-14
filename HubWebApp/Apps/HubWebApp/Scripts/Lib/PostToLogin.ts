import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { HubAppApi } from "./Api/HubAppApi";

export class PostToLogin {
    constructor(private readonly hubApi: HubAppApi) {
    }

    execute(credentials: ILoginCredentials, authKey: string) {
        let form = <HTMLFormElement>document.createElement('form');
        form.action = this.hubApi.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        let userNameInput = this.createInput('UserName', credentials.UserName, 'text');
        let passwordInput = this.createInput('Password', credentials.Password, 'password');
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