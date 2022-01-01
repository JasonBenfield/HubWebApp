import { AppApiFactory } from "@jasonbenfield/sharedwebapp/Api/AppApiFactory";
import { ModalErrorComponent } from "@jasonbenfield/sharedwebapp/Error/ModalErrorComponent";
import { ModalErrorComponentView } from "@jasonbenfield/sharedwebapp/Error/ModalErrorComponentView";
import { HubAppApi } from "../Hub/Api/HubAppApi";

export class Apis {
    private readonly modalError: ModalErrorComponent;

    constructor(modalError: ModalErrorComponentView) {
        this.modalError = new ModalErrorComponent(modalError);
    }

    hub() {
        let apiFactory = new AppApiFactory(HubAppApi, this.modalError)
        return apiFactory.api(HubAppApi);
    }
}