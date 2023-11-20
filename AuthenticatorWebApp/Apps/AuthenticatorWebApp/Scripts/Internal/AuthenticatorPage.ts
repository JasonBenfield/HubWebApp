import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { AuthenticatorAppClient } from "../Lib/Http/AuthenticatorAppClient";
import { AppClients } from "./AppClients";
import { AuthenticatorPageView } from "./AuthenticatorPageView";

export class AuthenticatorPage extends BasicPage {
    protected readonly authClient: AuthenticatorAppClient;

    constructor(view: AuthenticatorPageView) {
        const authClient = new AppClients(view.modalError).Authenticator();
        super(authClient, view);
        this.authClient = authClient;
    }
}