import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceResultType } from '../../../Hub/Api/ResourceResultType';
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { Row } from "XtiShared/Grid/Row";
import { ColumnCss } from "XtiShared/ColumnCss";

export class ResourceComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Resource');
        this.alert = this.addCardAlert().alert;
        let listGroup = this.addListGroup();
        let row = listGroup
            .addItem()
            .addContent(new Row());
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.resultType = row.addColumn()
            .addContent(new TextSpan());
        this.anonListItem = listGroup.addItem();
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan('Anonymous is Allowed'));
        this.anonListItem.hide();
    }

    private resourceID: number;

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
        this.resourceName.setText('');
        this.resultType.setText('');
        this.anonListItem.hide();
    }

    private readonly alert: MessageAlert;
    private readonly resourceName: TextSpan;
    private readonly resultType: TextSpan;
    private readonly anonListItem: IListItem;

    async refresh() {
        let resource = await this.getResource(this.resourceID);
        this.resourceName.setText(resource.Name);
        if (resource.IsAnonymousAllowed) {
            this.anonListItem.show();
        }
        else {
            this.anonListItem.hide();
        }
        let resultType = ResourceResultType.values.value(resource.ResultType.Value);
        let resultTypeText: string;
        if (
            resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
        ) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resultType.DisplayText;
        }
        this.resultType.setText(resultTypeText);
    }

    private async getResource(resourceID: number) {
        let resource: IResourceModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resource = await this.hubApi.Resource.GetResource({
                    VersionKey: 'Current',
                    ResourceID: resourceID
                });
            }
        );
        return resource;
    }
}