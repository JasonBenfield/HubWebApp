import { DefaultEvent } from "XtiShared/Events";
import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { ColumnCss } from "XtiShared/ColumnCss";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceResultType } from "../../../Hub/Api/ResourceResultType";

export class ResourceListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Resources');
        this.alert = this.addCardAlert().alert;
        this.resources = this.addButtonListGroup();
        this.resources.itemClicked.register(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: IListItem) {
        this._resourceSelected.invoke(item.getData<IResourceModel>());
    }

    private readonly alert: MessageAlert;
    private readonly resources: CardButtonListGroup;

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    private readonly _resourceSelected = new DefaultEvent<IResourceModel>(this);
    readonly resourceSelected = this._resourceSelected.handler();

    async refresh() {
        let resources = await this.getResources();
        this.resources.setItems(
            resources,
            (sourceItem, listItem) => {
                listItem.setData(sourceItem);
                let row = listItem.addContent(new Row());
                row.addColumn()
                    .configure(c => c.setColumnCss(ColumnCss.xs(8)))
                    .addContent(new TextSpan(sourceItem.Name));
                let resultType = ResourceResultType.values.value(sourceItem.ResultType.Value);
                let resultTypeText: string;
                if (
                    resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
                ) {
                    resultTypeText = '';
                }
                else {
                    resultTypeText = resultType.DisplayText;
                }
                row.addColumn()
                    .addContent(new TextSpan(resultTypeText));
            }
        );
        if (resources.length === 0) {
            this.alert.danger('No Resources were Found');
        }
    }

    private async getResources() {
        let resources: IResourceModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resources = await this.hubApi.ResourceGroup.GetResources({
                    VersionKey: 'Current',
                    GroupID: this.groupID
                });
            }
        );
        return resources;
    }
}