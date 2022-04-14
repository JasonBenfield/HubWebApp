import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ResourceGroupListCard {
    private readonly alert: MessageAlert;
    private readonly resourceGroups: ListGroup;
    readonly resourceGroupClicked: IEventHandler<ResourceGroupListItem>;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceGroupListCardView
    ) {
        new TextBlock('Resource Groups', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.resourceGroups = new ListGroup(this.view.resourceGroups);
        this.resourceGroupClicked = this.resourceGroups.itemClicked;
    }

    async refresh() {
        let resourceGroups = await this.getResourceGroups();
        this.resourceGroups.setItems(
            resourceGroups,
            (sourceItem: IResourceGroupModel, listItem: ResourceGroupListItemView) =>
                new ResourceGroupListItem(sourceItem, listItem)
        );
        if (resourceGroups.length === 0) {
            this.alert.danger('No Resource Groups were Found');
        }
    }

    private getResourceGroups() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.App.GetResourceGroups()
        );
    }
}