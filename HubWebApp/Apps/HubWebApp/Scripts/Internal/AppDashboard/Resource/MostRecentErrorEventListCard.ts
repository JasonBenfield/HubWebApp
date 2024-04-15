import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";
import { AppLogEntry } from "../../../Lib/AppLogEntry";

export class MostRecentErrorEventListCard {
    private readonly alert: MessageAlert;
    private readonly errorEvents: ListGroup<EventListItem, EventListItemView>;

    private resourceID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: MostRecentErrorEventListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Most Recent Errors');
        this.alert = new CardAlert(view.alert).alert;
        this.errorEvents = new ListGroup(view.errorEvents);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        const sourceErrorEvents = await this.getErrorEvents();
        const errorEvents = sourceErrorEvents.map(e => new AppLogEntry(e));
        this.errorEvents.setItems(
            errorEvents,
            (sourceItem, listItem) => new EventListItem(sourceItem, listItem)
        );
        if (errorEvents.length === 0) {
            this.alert.danger('No Errors were Found');
        }
    }

    private getErrorEvents() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Resource.GetMostRecentErrorEvents({
                VersionKey: 'Current',
                ResourceID: this.resourceID,
                HowMany: 10
            })
        );
    }
}