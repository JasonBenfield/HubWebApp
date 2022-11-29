import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ResourceListCardView } from "./ResourceListCardView";
import { ResourceListItem } from "./ResourceListItem";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListCard {
    private readonly alert: MessageAlert;
    private readonly resources: ListGroup<ResourceListItem, ResourceListItemView>;

    private groupID: number;

    private readonly _resourceSelected = new DefaultEvent<IResourceModel>(this);
    readonly resourceSelected = this._resourceSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        view: ResourceListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Resources');
        this.alert = new CardAlert(view.alert).alert;
        this.resources = new ListGroup(view.resources);
        this.resources.registerItemClicked(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ResourceListItem) {
        this._resourceSelected.invoke(item.resource);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const resources = await this.getResources();
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
            () => this.hubApi.ResourceGroup.GetResources({
                VersionKey: 'Current',
                GroupID: this.groupID
            })
        );
    }
}