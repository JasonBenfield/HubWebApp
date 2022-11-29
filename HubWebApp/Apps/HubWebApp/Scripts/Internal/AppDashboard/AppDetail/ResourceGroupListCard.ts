import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ResourceGroupListCard {
    private readonly alert: MessageAlert;
    private readonly resourceGroups: ListGroup<ResourceGroupListItem, ResourceGroupListItemView>;
    private readonly _resourceGroupClicked = new DefaultEvent<ResourceGroupListItem>(this);
    readonly resourceGroupClicked = this._resourceGroupClicked;

    constructor(
        private readonly hubApi: HubAppApi,
        view: ResourceGroupListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resource Groups');
        this.alert = new CardAlert(view.alert).alert;
        this.resourceGroups = new ListGroup(view.resourceGroups);
        this.resourceGroups.registerItemClicked(this.onResourceGroupClicked.bind(this));
    }

    private onResourceGroupClicked(resourceGroup: ResourceGroupListItem) {
        this._resourceGroupClicked.invoke(resourceGroup);
    }

    async refresh() {
        const resourceGroups = await this.getResourceGroups();
        this.resourceGroups.setItems(
            resourceGroups,
            (sourceItem, listItem) =>
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