import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceGroupListItem } from "../ResourceGroupListItem";
import { ResourceGroupListItemView } from "../ResourceGroupListItemView";
import { ResourceGroupListCardView } from "./ResourceGroupListCardView";

export class ResourceGroupListCard {
    private readonly _resourceSelected = new DefaultEvent<IResourceGroupModel>(this);
    readonly resourceGroupSelected = this._resourceSelected.handler();

    private readonly alert: MessageAlert;
    private readonly requests: ListGroup<ResourceGroupListItem, ResourceGroupListItemView>;

    private modCategoryID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceGroupListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resource Groups');
        this.alert = new CardAlert(view.alert).alert;
        this.requests = new ListGroup(view.requests);
        this.requests.registerItemClicked(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ResourceGroupListItem) {
        this._resourceSelected.invoke(item.group);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        let resourceGroups = await this.getResourceGroups();
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