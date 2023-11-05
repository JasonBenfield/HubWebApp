import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { AuthenticatorAppClient } from "../Lib/Http/AuthenticatorAppClient";
import { AppClients } from "./AppClients";
import { AuthenticatorPageView } from "./AuthenticatorPageView";

export class AuthenticatorPage extends BasicPage {
    protected readonly defaultClient: AuthenticatorAppClient;

    constructor(view: AuthenticatorPageView) {
        super(new AppClients(view.modalError).Authenticator(), view);
    }
}