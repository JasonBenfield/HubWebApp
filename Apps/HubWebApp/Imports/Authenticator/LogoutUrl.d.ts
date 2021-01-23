import { AuthenticatorAppApi } from './Api/AuthenticatorAppApi';
export declare class LogoutUrl implements ILogoutUrl {
    private readonly authenticator;
    constructor(authenticator: AuthenticatorAppApi);
    value(): string;
}
