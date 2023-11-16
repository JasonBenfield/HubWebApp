import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ODataExpandedLogEntryColumnsBuilder } from "../../../Lib/Http/ODataExpandedLogEntryColumnsBuilder";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { LogEntryQueryPanelView } from "./LogEntryQueryPanelView";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class LogEntryQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedLogEntry>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: LogEntryQueryPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
        const columns = new ODataExpandedLogEntryColumnsBuilder(this.view.columns);
        columns.EventID.require();
        columns.Severity.setDisplayText('Severity');
        const options = new ODataComponentOptionsBuilder<IExpandedLogEntry>('hub_logEntries', columns);
        options.query.select.addFields(
            columns.TimeOccurred,
            columns.Severity,
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
        const installationIDText = url.getQueryValue('InstallationID');
        const installationID = installationIDText ? Number(installationIDText) : null;
        options.saveChanges({
            select: true,
            filter: !requestID,
            orderby: true
        });
        options.setODataClient(
            new ApiODataClient(hubClient.LogEntryQuery, { RequestID: requestID, InstallationID: installationID })
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onDataCellClicked.bind(this));
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private onDataCellClicked(eventArgs: ODataCellClickedEventArgs) {
        const logEntryID: number = eventArgs.record['EventID'];
        this.hubClient.Logs.LogEntry.open({ LogEntryID: logEntryID });
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}