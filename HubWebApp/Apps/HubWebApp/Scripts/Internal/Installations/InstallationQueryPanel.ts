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
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { InstallationDropdownView } from "./InstallationDropdownView";
import { InstallationDropdown } from "./InstallationDropdown";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";
import { ModalMessageAlert } from "@jasonbenfield/sharedwebapp/Components/ModalMessageAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { InstallationDataRow } from "./InstallationDataRow";
import { Queryable } from "@jasonbenfield/sharedwebapp/OData/Types";

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
    private readonly modalAlert: ModalMessageAlert;

    constructor(private readonly hubApi: HubAppApi, private readonly view: InstallationQueryPanelView) {
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
                listItem.setHref(hubApi.Installations.Index.getUrl({ QueryType: queryType.Value }))
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
        const dropdownColumn = options.startColumns.add('Dropdown', this.view.dropdownColumn);
        dropdownColumn.setDisplayText('');
        dropdownColumn.setCreateDataCell(
            (rowIndex, column, record, formatter, view: InstallationDropdownView) => new InstallationDropdown(hubApi, rowIndex, column, record, view)
        );
        options.saveChanges({
            select: true,
            filter: true,
            orderby: true
        });
        options.setODataClient(
            new ApiODataClient(hubApi.InstallationQuery, { QueryType: selectedQueryType.Value })
        );
        options.setCreateDataRow(
            (rowIndex, columns, record: Queryable<IExpandedInstallation>, view) =>
                new InstallationDataRow(rowIndex, columns, record, view)
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onCellClicked.bind(this));
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.modalAlert = new ModalMessageAlert(view.modalAlert);
    }

    private onCellClicked(args: ODataCellClickedEventArgs) {
        if (this.view.isDeleteButton(args.element)) {
            const installationID = args.record['InstallationID'] as number;
            this.modalAlert.alert(
                async (a) => {
                    a.info('Deleting...');
                    await this.hubApi.Installations.RequestDelete({
                        InstallationID: installationID
                    });
                    a.success('The installation has been scheduled for deletion.');
                    new DelayedAction(
                        () => a.clear(),
                        2000
                    ).execute();
                    this.odataComponent.refresh();
                }
            );
        }
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    refresh() { this.odataComponent.refresh(); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}