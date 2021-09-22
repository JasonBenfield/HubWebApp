import { DefaultEvent } from "XtiShared/Events";
import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupListItem } from "../ResourceGroupListItem";

export class ResourceGroupListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Resource Groups');
        this.alert = this.addCardAlert().alert;
        this.requests = this.addButtonListGroup();
    }

    private readonly _resourceSelected = new DefaultEvent<IResourceGroupModel>(this);
    readonly resourceGroupSelected = this._resourceSelected.handler();

    protected onItemSelected(item: IListItem) {
        this._resourceSelected.invoke(item.getData<IResourceGroupModel>());
    }

    private readonly alert: MessageAlert;
    private readonly requests: CardButtonListGroup;

    private modCategoryID: number;

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        let resourceGroups = await this.getResourceGroups();
        this.requests.setItems(
            resourceGroups,
            (sourceItem, listItem) => {
                listItem.setData(sourceItem);
                listItem.addContent(new ResourceGroupListItem(sourceItem));
            }
        );
        if (resourceGroups.length === 0) {
            this.alert.danger('No Resource Groups were Found');
        }
    }

    private async getResourceGroups() {
        let resourceGroup: IResourceGroupModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resourceGroup = await this.hubApi.ModCategory.GetResourceGroups(this.modCategoryID);
            }
        );
        return resourceGroup;
    }
}