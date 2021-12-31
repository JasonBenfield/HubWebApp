import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceListCardView } from "./ResourceListCardView";
import { ResourceListItem } from "./ResourceListItem";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListCard {
    private readonly alert: MessageAlert;
    private readonly resources: ListGroup;

    private groupID: number;

    private readonly _resourceSelected = new DefaultEvent<IResourceModel>(this);
    readonly resourceSelected = this._resourceSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceListCardView
    ) {
        new TextBlock('Resources', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.resources = new ListGroup(this.view.resources);
        this.resources.itemClicked.register(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ResourceListItem) {
        this._resourceSelected.invoke(item.resource);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        let resources = await this.getResources();
        this.resources.setItems(
            resources,
            (sourceItem: IResourceModel, listItem: ResourceListItemView) =>
                new ResourceListItem(sourceItem, listItem)
        );
        if (resources.length === 0) {
            this.alert.danger('No Resources were Found');
        }
    }

    private async getResources() {
        let resources: IResourceModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                resources = await this.hubApi.ResourceGroup.GetResources({
                    VersionKey: 'Current',
                    GroupID: this.groupID
                });
            }
        );
        return resources;
    }
}