import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { AppResource } from "../../../Lib/AppResource";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceListCardView } from "./ResourceListCardView";
import { ResourceListItem } from "./ResourceListItem";
import { ResourceListItemView } from "./ResourceListItemView";

type Events = { resourceSelected: AppResource };

export class ResourceListCard {
    private readonly alert: IMessageAlert;
    private readonly resources: ListGroup<ResourceListItem, ResourceListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { resourceSelected: null });
    readonly when = this.eventSource.when;
    private groupID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resources');
        this.alert = new CardAlert(view.alert);
        this.resources = new ListGroup(view.resources);
        this.resources.when.itemClicked.then(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ResourceListItem) {
        this.eventSource.events.resourceSelected.invoke(item.resource);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const sourceResources = await this.getResources();
        const resources = sourceResources.map(r => new AppResource(r));
        this.resources.setItems(
            resources,
            (sourceItem, listItem) => new ResourceListItem(sourceItem, listItem)
        );
        if (resources.length === 0) {
            this.alert.danger('No Resources were Found');
        }
    }

    private getResources() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ResourceGroup.GetResources({
                VersionKey: 'Current',
                GroupID: this.groupID
            })
        );
    }
}