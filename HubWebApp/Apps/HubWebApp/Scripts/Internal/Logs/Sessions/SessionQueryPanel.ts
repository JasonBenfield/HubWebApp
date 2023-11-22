import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ODataColumn } from "@jasonbenfield/sharedwebapp/OData/ODataColumn";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { ODataLinkRow } from "@jasonbenfield/sharedwebapp/OData/ODataLinkRow";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ODataExpandedSessionColumnsBuilder } from "../../../Lib/Http/ODataExpandedSessionColumnsBuilder";
import { SessionQueryPanelView } from "./SessionQueryPanelView";
import { ODataRefreshedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataRefreshedEventArgs";
import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { Url } from "@jasonbenfield/sharedwebapp/Url";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class SessionQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedSession>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: SessionQueryPanelView) {
        const columns = new ODataExpandedSessionColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedSession>('hub_sessions', columns);
        columns.SessionID.require();
        columns.UserID.require();
        options.query.select.addFields(
            columns.TimeSessionStarted,
            columns.UserName,
            columns.RequestCount,
            columns.LastRequestTime
        );
        options.query.orderBy.addDescending(columns.TimeSessionStarted);
        options.saveChanges();
        options.setDefaultODataClient(hubClient.SessionQuery, { args: {} });
        options.setCreateLinkRow(
            (rowIndex: number, columns: ODataColumn[], record: any, row: ODataLinkRow) => {
                const sessionID: number = record['SessionID'];
                row.setHref(this.hubClient.Logs.Session.getUrl({ SessionID: sessionID }));
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