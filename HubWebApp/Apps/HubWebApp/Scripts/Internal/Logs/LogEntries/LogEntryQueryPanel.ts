import { Awaitable } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Awaitable";
import { Command } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/Command";
import { ApiODataClient } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ApiODataClient";
import { ODataComponent } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponentOptionsBuilder";
import { Queryable } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/Types";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedLogEntryColumnsBuilder } from "../../../Lib/Api/ODataExpandedLogEntryColumnsBuilder";
import { LogEntryQueryPanelView } from "./LogEntryQueryPanelView";

interface IResult {
    menuRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class LogEntryQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedLogEntry>;

    constructor(hubApi: HubAppApi, private readonly view: LogEntryQueryPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
        const columns = new ODataExpandedLogEntryColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedLogEntry>('hub_logEntries', columns);
        options.query.select.addFields(
            columns.TimeOccurred,
            columns.SeverityText,
            columns.Caption,
            columns.Message,
            columns.Detail,
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
        options.query.orderBy.addDescending(columns.TimeOccurred);
        options.saveChanges();
        options.setODataClient(
            new ApiODataClient(hubApi.LogEntryQuery, {})
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