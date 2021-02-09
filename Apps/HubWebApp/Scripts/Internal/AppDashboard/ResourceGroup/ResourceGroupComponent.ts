import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { Row } from "XtiShared/Grid/Row";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class ResourceGroupComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Resource Group');
        this.alert = this.addCardAlert().alert;
        let listGroup = this.addListGroup();
        let row = listGroup
            .addItem()
            .addContent(new Row());
        this.groupName = row.addColumn()
            .addContent(new TextSpan());
        this.anonListItem = listGroup.addItem();
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan('Anonymous is Allowed'));
        this.anonListItem.hide();
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    private readonly alert: MessageAlert;
    private readonly groupName: TextSpan;
    private readonly anonListItem: IListItem;

    async refresh() {
        let group = await this.getResourceGroup(this.groupID);
        this.groupName.setText(group.Name);
        if (group.IsAnonymousAllowed) {
            this.anonListItem.show();
        }
        else {
            this.anonListItem.hide();
        }
    }

    private async getResourceGroup(groupID: number) {
        let group: IResourceGroupModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                group = await this.hubApi.ResourceGroup.GetResourceGroup(groupID);
            }
        );
        return group;
    }
}