import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { AppLogEntry } from "../../../Lib/AppLogEntry";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "./MostRecentErrorEventListCardView";

export class MostRecentErrorEventListCard {
    private readonly alert: IMessageAlert;
    private readonly errorEvents: ListGroup<EventListItem, EventListItemView>;

    constructor(private readonly hubApi: HubAppClient, view: MostRecentErrorEventListCardView) {
        new TextComponent(view.titleHeader).setText('Most Recent Errors');
        this.alert = new CardAlert(view.alert);
        this.alert.disableAutoScrollIntoView();
        this.errorEvents = new ListGroup(view.errorEvents);
    }

    async refresh() {
        const sourceErrorEvents = await this.getErrorEvents();
        const errorEvents = sourceErrorEvents.map(e => new AppLogEntry(e));
        this.errorEvents.setItems(
            errorEvents,
            (errorEvent, itemView) =>
                new EventListItem(errorEvent, itemView)
        );
        if (errorEvents.length === 0) {
            this.alert.danger('No Errors were Found');
        }
    }

    private getErrorEvents() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.App.GetMostRecentErrorEvents(10)
        );
    }
}