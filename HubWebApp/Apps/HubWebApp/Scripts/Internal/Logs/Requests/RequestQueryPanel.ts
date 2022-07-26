import { Awaitable } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Awaitable";
import { Command } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/Command";
import { ApiODataClient } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ApiODataClient";
import { ODataComponent } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponentOptionsBuilder";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedRequestColumnsBuilder } from "../../../Lib/Api/ODataExpandedRequestColumnsBuilder";
import { RequestQueryPanelView } from "./RequestQueryPanelView";

interface IResult {
    menuRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class RequestQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedRequest>;

    constructor(hubApi: HubAppApi, private readonly view: RequestQueryPanelView) {
        const columns = new ODataExpandedRequestColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedRequest>('hub_requests', columns);
        options.query.select.addFields(
            columns.TimeStarted,
            columns.TimeElapsed,
            columns.UserName,
            columns.Path,
            columns.AppName,
            columns.ResourceGroupName,
            columns.ResourceName,
            columns.InstallLocation,
            columns.IsCurrentInstallation,
            columns.ModCategoryName,
            columns.ModDisplayText,
            columns.VersionKey
        );
        options.query.orderBy.addDescending(columns.TimeStarted);
        options.saveChanges();
        options.setODataClient(
            new ApiODataClient(hubApi.RequestQuery, {})
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}