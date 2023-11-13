import { AppClientFactory } from "@jasonbenfield/sharedwebapp/Http/AppClientFactory";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { AuthenticatorAppClient } from "../Lib/Http/AuthenticatorAppClient";
import { HubAppClient } from "@hub/Http/HubAppClient";

export class AppClients {
    private readonly appClientFactory: AppClientFactory;

    constructor(modalError: ModalErrorView) {
        this.appClientFactory = new AppClientFactory(modalError);
    }

    Authenticator() {
        return this.appClientFactory.create(AuthenticatorAppClient);
    }

    Hub() {
        return this.appClientFactory.create(HubAppClient);
    }
}