import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { AppResourceGroup } from "../../../Lib/AppResourceGroup";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

type Events = { resourceGroupClicked: AppResourceGroup };

export class ResourceGroupListCard {
    private readonly alert: IMessageAlert;
    private readonly resourceGroups: ListGroup<ResourceGroupListItem, ResourceGroupListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { resourceGroupClicked: new AppResourceGroup() });
    readonly when = this.eventSource.when;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceGroupListCardView
    ) {
        new TextComponent(view.titleHeader).setText("Resource Groups");
        this.alert = new CardAlert(view.alert);
        this.alert.disableAutoScrollIntoView();
        this.resourceGroups = new ListGroup(view.resourceGroups);
        this.resourceGroups.when.itemClicked.then(this.onResourceGroupClicked.bind(this));
    }

    private onResourceGroupClicked(listItem: ResourceGroupListItem) {
        this.eventSource.events.resourceGroupClicked.invoke(listItem.group);
    }

    async refresh() {
        const sourceResourceGroups = await this.getResourceGroups();
        const resourceGroups = sourceResourceGroups.map(rg => new AppResourceGroup(rg));
        this.resourceGroups.setItems(
            resourceGroups,
            (sourceItem, listItem) =>
                new ResourceGroupListItem(sourceItem, listItem)
        );
        if (resourceGroups.length === 0) {
            this.alert.danger("No Resource Groups were Found");
        }
    }

    private getResourceGroups() {
        return this.alert.infoAction(
            "Loading...",
            () => this.hubClient.App.GetResourceGroups()
        );
    }
}