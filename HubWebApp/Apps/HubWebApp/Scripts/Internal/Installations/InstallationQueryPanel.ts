import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";
import { Url } from "@jasonbenfield/sharedwebapp/Url";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { InstallationQueryType } from "../../Lib/Http/InstallationQueryType";
import { ODataExpandedInstallationColumnsBuilder } from "../../Lib/Http/ODataExpandedInstallationColumnsBuilder";
import { InstallationDataRow } from "./InstallationDataRow";
import { InstallationQueryPanelView } from "./InstallationQueryPanelView";
import { GridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class InstallationQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly queryTypes: ListGroup<TextComponent, TextLinkListGroupItemView>;
    private readonly odataComponent: ODataComponent<IExpandedInstallation>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallationQueryPanelView) {
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.queryTypes = new ListGroup(this.view.queryTypes);
        const selectedQueryTypeText = Url.current().getQueryValue('QueryType');
        const selectedQueryType = InstallationQueryType.values.value(
            selectedQueryTypeText ? Number(selectedQueryTypeText) : 0
        );
        this.queryTypes.setItems(
            InstallationQueryType.values.all,
            (queryType, itemView) => {
                const listItem = new TextLinkComponent(itemView);
                listItem.setText(queryType.DisplayText);
                listItem.setHref(hubClient.Installations.Index.getUrl({ QueryType: queryType.Value }))
                if (selectedQueryType === queryType) {
                    itemView.active();
                }
                return listItem;
            }
        );
        const columns = new ODataExpandedInstallationColumnsBuilder(this.view.columns);
        columns.InstallationID.require();
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
            new ApiODataClient(hubClient.InstallationQuery, { QueryType: selectedQueryType.Value })
        );
        options.setCreateDataRow(
            (rowIndex, columns, record: Queryable<IExpandedInstallation>, view: GridRowView) =>
                new InstallationDataRow(rowIndex, columns, record, view)
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onCellClicked.bind(this));
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private onCellClicked(args: ODataCellClickedEventArgs) {
        const installationID: number = args.record['InstallationID'];
        this.hubClient.Installations.Installation.open({ InstallationID: installationID });
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}