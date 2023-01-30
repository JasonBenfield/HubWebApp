import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedSessionColumnsBuilder } from "../../../Lib/Api/ODataExpandedSessionColumnsBuilder";
import { SessionQueryPanelView } from "./SessionQueryPanelView";

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

    constructor(private readonly hubApi: HubAppApi, private readonly view: SessionQueryPanelView) {
        const columns = new ODataExpandedSessionColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedSession>('hub_sessions', columns);
        columns.SessionID.require();
        columns.UserID.require();
        options.query.select.addFields(
            columns.TimeStarted,
            columns.UserName,
            columns.RequestCount,
            columns.LastRequestTime
        );
        options.query.orderBy.addDescending(columns.TimeStarted);
        options.saveChanges();
        options.setODataClient(
            new ApiODataClient(hubApi.SessionQuery, {})
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onDataCellClicked.bind(this));
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private onDataCellClicked(eventArgs: ODataCellClickedEventArgs) {
        const sessionID: number = eventArgs.record['SessionID'];
        this.hubApi.Logs.Session.open({ SessionID: sessionID });
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}