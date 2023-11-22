import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { ODataLinkRow } from "@jasonbenfield/sharedwebapp/OData/ODataLinkRow";
import { ODataRefreshedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataRefreshedEventArgs";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ODataExpandedLogEntryColumnsBuilder } from "../../../Lib/Http/ODataExpandedLogEntryColumnsBuilder";
import { LogEntryQueryPanelView } from "./LogEntryQueryPanelView";

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
        columns.Detail.setFormatter({
            format: (col, record) => {
                const value: string = record[col.columnName];
                return value && value.length > 200 ? value.substring(0, 200) : value;
            }
        });
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
        options.setDefaultODataClient(
            hubClient.LogEntryQuery,
            { args: { RequestID: requestID, InstallationID: installationID } }
        );
        options.setCreateLinkRow(
            (rowIndex: number, columns: ODataColumn[], record: any, row: ODataLinkRow) => {
                const logEntryID: number = record['EventID'];
                row.setHref(this.hubClient.Logs.LogEntry.getUrl({ LogEntryID: logEntryID }));
            }
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.refreshed.then(this.onRefreshed.bind(this));
        const page = Url.current().getQueryValue('page');
        if (page) {
            this.odataComponent.setCurrentPage(Number(page));
        }
        new Command(this.menu.bind(this)).add(view.menuButton);
    }
    
    private onRefreshed(args: ODataRefreshedEventArgs) {
        const page = args.page > 1 ? args.page.toString() : '';
        const url = UrlBuilder.current();
        const queryPage = url.getQueryValue('page');
        if (page !== queryPage) {
            if (page) {
                url.replaceQuery('page', page);
            }
            else {
                url.removeQuery('page');
            }
            history.replaceState({}, '', url.value());
        }
    }
    
    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}