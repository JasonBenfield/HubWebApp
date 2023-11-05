import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { BasicPageView } from "@jasonbenfield/sharedwebapp/Views/BasicPageView";
import { HubAppClient } from "../Lib/Http/HubAppClient";
import { AppClients } from "./AppClients";

export class HubPage extends BasicPage {
    protected readonly defaultClient: HubAppClient;

    constructor(view: BasicPageView) {
        super(new AppClients(view.modalError).Hub(), view);
    }
}