import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedLogEntryColumnsBuilder } from "../../../Lib/Api/ODataExpandedLogEntryColumnsBuilder";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
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
        columns.SeverityText.setDisplayText('Severity');
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
        const url = Url.current();
        const requestIDText = url.getQueryValue('RequestID');
        const requestID = requestIDText ? Number(requestIDText) : null;
        options.saveChanges({
            select: true,
            filter: !requestID,
            orderby: true
        });
        options.setODataClient(
            new ApiODataClient(hubApi.LogEntryQuery, { RequestID: requestID })
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