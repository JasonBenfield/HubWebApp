import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { ODataRefreshedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataRefreshedEventArgs";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { LinkGridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ODataExpandedRequestColumnsBuilder } from "../../../Lib/Http/ODataExpandedRequestColumnsBuilder";
import { RequestDataRow } from "./RequestDataRow";
import { RequestQueryPanelView } from "./RequestQueryPanelView";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class RequestQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedRequest>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: RequestQueryPanelView) {
        const columns = new ODataExpandedRequestColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedRequest>('hub_requests', columns);
        columns.RequestID.require();
        columns.Succeeded.require();
        options.query.select.addFields(
            columns.RequestTimeStarted,
            columns.RequestTimeElapsed,
            columns.UserName,
            columns.Path,
            columns.AppKey,
            columns.ResourceGroupName,
            columns.ResourceName,
            columns.InstallLocation,
            columns.IsCurrentInstallation,
            columns.ModCategoryName,
            columns.ModDisplayText,
            columns.VersionKey
        );
        options.query.orderBy.addDescending(columns.RequestTimeStarted);
        const url = Url.current();
        const sessionID = url.query.getNumberValue('SessionID');
        const installationID = url.query.getNumberValue('InstallationID');
        const sourceRequestID = url.query.getNumberValue('SourceRequestID');
        options.saveChanges({
            select: true,
            filter: !sessionID,
            orderby: true
        });
        options.setDefaultODataClient(
            hubClient.RequestQuery,
            {
                args: {
                    SessionID: sessionID,
                    InstallationID: installationID,
                    SourceRequestID: sourceRequestID
                }
            }
        );
        options.setCreateDataRow(
            (rowIndex: number, columns: ODataColumn[], record: any, view: LinkGridRowView) =>
                new RequestDataRow(hubClient, rowIndex, columns, record, view)
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.refreshed.then(this.onRefreshed.bind(this));
        const page = Url.current().query.getNumberValue('page');
        if (page) {
            this.odataComponent.setCurrentPage(page);
        }
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private onRefreshed(args: ODataRefreshedEventArgs) {
        const page = args.page > 1 ? args.page.toString() : '';
        const url = UrlBuilder.current();
        const queryPageValue = url.query.getNumberValue('page');
        const queryPage = queryPageValue > 1 ? queryPageValue.toString() : '';
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