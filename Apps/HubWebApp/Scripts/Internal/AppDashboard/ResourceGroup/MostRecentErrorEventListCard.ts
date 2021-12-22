import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";

export class MostRecentErrorEventListCard {
    private readonly alert: MessageAlert;
    private readonly errorEvents: ListGroup;

    private groupID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: MostRecentErrorEventListCardView
    ) {
        new CardTitleHeader('Most Recent Errors', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.errorEvents = new ListGroup(this.view.errorEvents);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
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
                errorEvents = await this.hubApi.ResourceGroup.GetMostRecentErrorEvents({
                    VersionKey: 'Current',
                    GroupID: this.groupID,
                    HowMany: 10
                });
            }
        );
        return errorEvents;
    }
}