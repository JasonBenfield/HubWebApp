import { BasicPage } from "@jasonbenfield/sharedwebapp/Components/BasicPage";
import { BasicPageView } from "@jasonbenfield/sharedwebapp/Views/BasicPageView";
import { HubAppApi } from "../Lib/Api/HubAppApi";
import { Apis } from "./Apis";

export class HubPage extends BasicPage {
    protected readonly defaultApi: HubAppApi;

    constructor(view: BasicPageView) {
        super(new Apis(view.modalError).Hub(), view);
    }
}