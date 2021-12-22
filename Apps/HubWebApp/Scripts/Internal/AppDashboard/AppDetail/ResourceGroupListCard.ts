import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ResourceGroupListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup;

    private readonly _resourceSelected = new DefaultEvent<IResourceGroupModel>(this);
    readonly resourceGroupSelected = this._resourceSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceGroupListCardView
    ) {
        new CardTitleHeader('Resource Groups', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.requests = new ListGroup(this.view.requests);
    }

    protected onItemSelected(item: ResourceGroupListItem) {
        this._resourceSelected.invoke(item.group);
    }

    async refresh() {
        let resourceGroups = await this.getResourceGroups();
        this.requests.setItems(
            resourceGroups,
            (sourceItem: IResourceGroupModel, listItem: ResourceGroupListItemView) =>
                new ResourceGroupListItem(sourceItem, listItem)
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
                resourceGroup = await this.hubApi.App.GetResourceGroups();
            }
        );
        return resourceGroup;
    }
}