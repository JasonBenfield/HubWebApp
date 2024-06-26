﻿import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { HubAppClient } from "./Http/HubAppClient";

export class PostToLogin {
    constructor(private readonly hubClient: HubAppClient) {
    }

    execute(authRequest: IAuthenticateRequest, authKey: string, authID: string) {
        const form = <HTMLFormElement>document.createElement('form');
        form.action = this.hubClient.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        const userNameInput = this.createInput('UserName', authRequest.UserName, 'text');
        const passwordInput = this.createInput('Password', authRequest.Password, 'password');
        const urlBuilder = UrlBuilder.current();
        const authKeyInput = this.createInput('AuthKey', authKey);
        const authIDInput = this.createInput('AuthID', authID);
        const returnKeyInput = this.createInput('ReturnKey', urlBuilder.query.getValue('returnKey'));
        form.append(
            userNameInput,
            passwordInput,
            authKeyInput,
            authIDInput,
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