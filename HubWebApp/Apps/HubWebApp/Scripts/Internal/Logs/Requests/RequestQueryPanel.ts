import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ODataExpandedRequestColumnsBuilder } from "../../../Lib/Api/ODataExpandedRequestColumnsBuilder";
import { RequestDataRow } from "./RequestDataRow";
import { RequestDropdown } from "./RequestDropdown";
import { RequestDropdownView } from "./RequestDropdownView";
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

    constructor(hubApi: HubAppApi, private readonly view: RequestQueryPanelView) {
        const columns = new ODataExpandedRequestColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedRequest>('hub_requests', columns);
        columns.RequestID.require();
        columns.Succeeded.require();
        options.setCreateDataRow(
            (rowIndex, columns, record: Queryable<IExpandedRequest>, view) =>
                new RequestDataRow(rowIndex, columns, record, view)
        );
        options.query.select.addFields(
            columns.RequestTimeStarted,
            columns.RequestTimeElapsed,
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
        options.query.orderBy.addDescending(columns.RequestTimeStarted);
        const dropdownColumn = options.startColumns.add('Dropdown', this.view.dropdownColumn);
        dropdownColumn.setDisplayText('');
        dropdownColumn.setCreateDataCell(
            (rowIndex, column, record, formatter, view: RequestDropdownView) => new RequestDropdown(hubApi, rowIndex, column, record, view)
        );
        const url = Url.current();
        const sessionIDText = url.getQueryValue('SessionID');
        const sessionID = sessionIDText ? Number(sessionIDText) : null;
        const installationIDText = url.getQueryValue('InstallationID');
        const installationID = installationIDText ? Number(installationIDText) : null;
        options.saveChanges({
            select: true,
            filter: !sessionID,
            orderby: true
        });
        options.setODataClient(
            new ApiODataClient(hubApi.RequestQuery, { SessionID: sessionID, InstallationID: installationID })
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