import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";

export class MostRecentErrorEventListCard {
    private readonly alert: MessageAlert;
    private readonly errorEvents: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: MostRecentErrorEventListCardView
    ) {
        new TextBlock('Most Recent Errors', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.errorEvents = new ListGroup(this.view.errorEvents);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        let errorEvents = await this.getErrorEvents();
        this.errorEvents.setItems(
            errorEvents,
            (sourceItem: IAppEventModel, listItem: EventListItemView) =>
                new EventListItem(sourceItem, listItem)
        );
        if (errorEvents.length === 0) {
            this.alert.danger('No Errors were Found');
        }
    }

    private async getErrorEvents() {
        let errorEvents: IAppEventModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                errorEvents = await this.hubApi.Resource.GetMostRecentErrorEvents({
                    VersionKey: 'Current',
                    ResourceID: this.resourceID,
                    HowMany: 10
                });
            }
        );
        return errorEvents;
    }
}