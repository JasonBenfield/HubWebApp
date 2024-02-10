import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { ODataRefreshedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataRefreshedEventArgs";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { UrlBuilder } from "@jasonbenfield/sharedwebapp/UrlBuilder";
import { GridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { ODataExpandedUserRoleColumnsBuilder } from "../../Lib/Http/ODataExpandedUserRoleColumnsBuilder";
import { UserRoleQueryPanelView } from "./UserRoleQueryPanelView";

interface IResult {
    menuRequested?: boolean;
    addRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    static addRequested() { return new Result({ addRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }

    get addRequested() { return this.result.addRequested; }
}

export class UserRoleQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly odataComponent: ODataComponent<IExpandedUserRole>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: UserRoleQueryPanelView) {
        const columns = new ODataExpandedUserRoleColumnsBuilder(this.view.columns);
        columns.UserRoleID.require();
        const options = new ODataComponentOptionsBuilder<IExpandedUserRole>('hub_userRoles', columns);
        options.setCreateLinkRow(
            (rowIndex, columns, record: Queryable<IExpandedUserRole>, row) => {
                row.setHref(hubClient.UserRoles.UserRole.getUrl({ UserRoleID: record.UserRoleID }));
            }
        );
        options.query.select.addFields(
            columns.UserName,
            columns.AppKey,
            columns.ModCategoryName,
            columns.ModDisplayText,
            columns.RoleDisplayText
        );
        options.saveChanges();
        options.setDefaultODataClient(
            this.hubClient.UserRoleQuery,
            { args: { AppID: Url.current().query.getNumberValue('AppID') } }
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
    
    async refresh() {
        this.odataComponent.refresh();
    }
    
    private menu() { this.awaitable.resolve(Result.menuRequested()); }
    
    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}