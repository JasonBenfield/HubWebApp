import { AppClientFactory } from "@jasonbenfield/sharedwebapp/Http/AppClientFactory";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { HubAppClient } from "../Lib/Http/HubAppClient";

export class AppClients {
    private readonly appClientFactory: AppClientFactory;

    constructor(modalError: ModalErrorView) {
        this.appClientFactory = new AppClientFactory(modalError);
    }

    Hub() {
        return this.appClientFactory.create(HubAppClient);
    }
}