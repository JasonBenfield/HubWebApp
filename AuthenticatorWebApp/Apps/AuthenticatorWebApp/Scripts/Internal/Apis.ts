import { AppApiFactory } from "@jasonbenfield/sharedwebapp/Api/AppApiFactory";
import { ModalErrorComponent } from "@jasonbenfield/sharedwebapp/Error/ModalErrorComponent";
import { ModalErrorComponentView } from "@jasonbenfield/sharedwebapp/Error/ModalErrorComponentView";
import { AuthenticatorAppApi } from "../Authenticator/Api/AuthenticatorAppApi";
import { HubAppApi } from "@hub/Api/HubAppApi";

export class Apis {
    private readonly modalError: ModalErrorComponent;

    constructor(modalError: ModalErrorComponentView) {
        this.modalError = new ModalErrorComponent(modalError);
    }

    Authenticator() {
        let apiFactory = new AppApiFactory(this.modalError)
        return apiFactory.api(AuthenticatorAppApi);
    }

    Hub() {
        let apiFactory = new AppApiFactory(this.modalError)
        return apiFactory.api(HubAppApi);
    }
}