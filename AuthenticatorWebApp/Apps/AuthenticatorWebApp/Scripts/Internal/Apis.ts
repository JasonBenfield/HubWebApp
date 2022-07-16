import { AppApiFactory } from "@jasonbenfield/sharedwebapp/Api/AppApiFactory";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { AuthenticatorAppApi } from "../Lib/Api/AuthenticatorAppApi";
import { HubAppApi } from "@hub/Api/HubAppApi";

export class Apis {
    constructor(private readonly modalError: ModalErrorView) {
    }

    Authenticator() {
        const apiFactory = new AppApiFactory(this.modalError)
        return apiFactory.api(AuthenticatorAppApi);
    }

    Hub() {
        const apiFactory = new AppApiFactory(this.modalError)
        return apiFactory.api(HubAppApi);
    }
}