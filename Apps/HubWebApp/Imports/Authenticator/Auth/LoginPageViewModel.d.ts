import { LoginComponentViewModel } from './LoginComponentViewModel';
import { PageViewModel } from 'XtiShared/PageViewModel';
import { AuthenticatorAppApi } from '../Api/AuthenticatorAppApi';
export declare class LoginPageViewModel extends PageViewModel {
    private readonly authenticatorApi;
    constructor(authenticatorApi: AuthenticatorAppApi);
    readonly loginComponent: LoginComponentViewModel;
}
