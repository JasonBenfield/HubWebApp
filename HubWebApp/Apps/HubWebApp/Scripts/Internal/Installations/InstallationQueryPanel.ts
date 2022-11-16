import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { InstallationQueryType } from "../../Lib/Api/InstallationQueryType";
import { ODataExpandedInstallationColumnsBuilder } from "../../Lib/Api/ODataExpandedInstallationColumnsBuilder";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { InstallationQueryPanelView } from "./InstallationQueryPanelView";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";

interface IResult {
    menuRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class InstallationQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly queryTypes: ListGroup;
    private readonly odataComponent: ODataComponent<IExpandedInstallation>;

    constructor(hubApi: HubAppApi, private readonly view: InstallationQueryPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.queryTypes = new ListGroup(this.view.queryTypes);
        const selectedQueryTypeText = Url.current().getQueryValue('QueryType');
        const selectedQueryType = InstallationQueryType.values.value(
            selectedQueryTypeText ? Number(selectedQueryTypeText) : 0
        );
        this.queryTypes.setItems(
            InstallationQueryType.values.all,
            (queryType, itemView: TextLinkListGroupItemView) => {
                const listItem = new TextLinkComponent(itemView);
                listItem.setText(queryType.DisplayText);
                listItem.setHref(hubApi.Installations.Index.getUrl({ QueryType: queryType.Value }))
                if (selectedQueryType === queryType) {
                    itemView.active();
                }
                return listItem;
            }
        );
        const columns = new ODataExpandedInstallationColumnsBuilder(this.view.columns);
        const options = new ODataComponentOptionsBuilder<IExpandedInstallation>('hub_installations', columns);
        options.query.select.addFields(
            columns.AppKey,
            columns.QualifiedMachineName,
            columns.Domain,
            columns.IsCurrent,
            columns.VersionStatusText,
            columns.VersionRelease,
            columns.VersionKey,
            columns.InstallationStatusDisplayText,
            columns.LastRequestDaysAgo,
            columns.RequestCount
        );
        options.query.orderBy.addDescending(columns.TimeInstallationAdded);
        options.saveChanges({
            select: true,
            filter: true,
            orderby: true
        });
        options.setODataClient(
            new ApiODataClient(hubApi.InstallationQuery, { QueryType: selectedQueryType.Value })
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