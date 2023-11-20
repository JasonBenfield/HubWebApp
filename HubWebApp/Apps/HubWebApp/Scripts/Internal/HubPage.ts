import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { BasicPageView } from "@jasonbenfield/sharedwebapp/Views/BasicPageView";
import { HubAppClient } from "../Lib/Http/HubAppClient";
import { AppClients } from "./AppClients";

export class HubPage extends BasicPage {
    protected readonly hubClient: HubAppClient;

    constructor(view: BasicPageView) {
        const hubClient = new AppClients(view.modalError).Hub();
        super(hubClient, view);
        this.hubClient = hubClient;
    }
}