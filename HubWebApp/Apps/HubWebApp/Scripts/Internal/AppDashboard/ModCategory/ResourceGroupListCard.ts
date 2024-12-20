﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { AppResourceGroup } from "../../../Lib/AppResourceGroup";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

type Events = { resourceGroupSelected: AppResourceGroup };

export class ResourceGroupListCard {
    private readonly eventSource = new EventSource<Events>(this, { resourceGroupSelected: null });
    readonly when = this.eventSource.when;

    private readonly alert: IMessageAlert;
    private readonly requests: ListGroup<ResourceGroupListItem, ResourceGroupListItemView>;

    private modCategoryID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceGroupListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resource Groups');
        this.alert = new CardAlert(view.alert);
        this.requests = new ListGroup(view.requests);
        this.requests.when.itemClicked.then(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ResourceGroupListItem) {
        this.eventSource.events.resourceGroupSelected.invoke(item.group);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        const sourceResourceGroups = await this.getResourceGroups();
        const resourceGroups = sourceResourceGroups.map(rg => new AppResourceGroup(rg));
        this.requests.setItems(
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
            () => this.hubClient.ModCategory.GetResourceGroups(this.modCategoryID)
        );
    }
}