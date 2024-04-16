import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { AuthenticatorAppClient } from "../Lib/Http/AuthenticatorAppClient";
import { AppClients } from "./AppClients";
import { AuthenticatorPageView } from "./AuthenticatorPageView";
import { HubAppClient } from "@hub/Http/HubAppClient";

export class AuthenticatorPage extends BasicPage {
    protected readonly authClient: AuthenticatorAppClient;
    protected readonly hubClient: HubAppClient;

    constructor(view: AuthenticatorPageView) {
        const apis = new AppClients(view.modalError);
        const authClient = apis.Authenticator();
        super(authClient, view);
        this.authClient = authClient;
        this.hubClient = apis.Hub();
    }
}