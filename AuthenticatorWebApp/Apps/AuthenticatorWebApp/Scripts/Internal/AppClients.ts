import { AppClientFactory } from "@jasonbenfield/sharedwebapp/Http/AppClientFactory";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { AuthenticatorAppClient } from "../Lib/Api/AuthenticatorAppClient";
import { HubAppClient } from "@hub/Api/HubAppClient";

export class AppClients {
    private readonly appClientFactory;

    constructor(modalError: ModalErrorView) {
        this.appClientFactory = new AppClientFactory(modalError);
    }

    Authenticator() {
        return this.appClientFactory.api(AuthenticatorAppClient);
    }

    Hub() {
        return this.appClientFactory.api(HubAppClient);
    }
}