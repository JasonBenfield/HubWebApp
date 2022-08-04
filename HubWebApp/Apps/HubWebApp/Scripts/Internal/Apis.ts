import { AppApiFactory } from "@jasonbenfield/sharedwebapp/Api/AppApiFactory";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { HubAppApi } from "../Lib/Api/HubAppApi";

export class Apis {
    constructor(private readonly modalError: ModalErrorView) {
    }

    Hub() {
        const apiFactory = new AppApiFactory(this.modalError);
        return apiFactory.api(HubAppApi);
    }
}