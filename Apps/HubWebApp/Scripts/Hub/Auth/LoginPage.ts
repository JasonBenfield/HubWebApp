﻿import { LoginPageViewModel } from "./LoginPageViewModel";
import { LoginComponent, LoginResult } from "./LoginComponent";
import { startup } from 'xtistart';

class LoginPage
{
    constructor(private readonly vm: LoginPageViewModel) {
        this.activateLoginComponent();
    }

    private readonly loginComponent = new LoginComponent(this.vm.loginComponent);

    private async activateLoginComponent() {
        let result = await this.loginComponent.activate();
        if (result instanceof LoginResult) {
            alert(`Login Component Result: ${result.token}`);
        }
    }
}
startup(
    () => new LoginPageViewModel(),
    vm => new LoginPage(<LoginPageViewModel>vm)
);