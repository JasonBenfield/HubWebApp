import { Awaitable } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Awaitable";
import { Command } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/Command";
import { ApiODataClient } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ApiODataClient";
import { ODataComponent } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/OData/ODataComponentOptionsBuilder";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedSessionColumnsBuilder } from "../../../Lib/Api/ODataExpandedSessionColumnsBuilder";
import { SessionQueryPanelView } from "./SessionQueryPanelView";

interface IResult {
    menuRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class SessionQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedSession>;

    constructor(hubApi: HubAppApi, private readonly view: SessionQueryPanelView) {
        const columns = new ODataExpandedSessionColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedSession>('hub_sessions', columns);
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
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}