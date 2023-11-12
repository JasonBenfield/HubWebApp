import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

type Events = { resourceGroupClicked: ResourceGroupListItem };

export class ResourceGroupListCard {
    private readonly alert: MessageAlert;
    private readonly resourceGroups: ListGroup<ResourceGroupListItem, ResourceGroupListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { resourceGroupClicked: null });
    readonly when = this.eventSource.when;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceGroupListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resource Groups');
        this.alert = new CardAlert(view.alert).alert;
        this.resourceGroups = new ListGroup(view.resourceGroups);
        this.resourceGroups.when.itemClicked.then(this.onResourceGroupClicked.bind(this));
    }

    private onResourceGroupClicked(resourceGroup: ResourceGroupListItem) {
        this.eventSource.events.resourceGroupClicked.invoke(resourceGroup);
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
            () => this.hubClient.App.GetResourceGroups()
        );
    }
}