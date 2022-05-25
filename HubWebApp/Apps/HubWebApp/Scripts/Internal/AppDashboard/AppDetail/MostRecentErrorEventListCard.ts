﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "./MostRecentErrorEventListCardView";

export class MostRecentErrorEventListCard {
    private readonly alert: MessageAlert;
    private readonly errorEvents: ListGroup;

    constructor(private readonly hubApi: HubAppApi, private readonly view: MostRecentErrorEventListCardView) {
        new TextBlock('Most Recent Errors', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.errorEvents = new ListGroup(this.view.errorEvents);
    }

    async refresh() {
        let errorEvents = await this.getErrorEvents();
        this.errorEvents.setItems(
            errorEvents,
            (errorEvent: IAppLogEntryModel, itemView: EventListItemView) =>
                new EventListItem(errorEvent, itemView)
        );
        if (errorEvents.length === 0) {
            this.alert.danger('No Errors were Found');
        }
    }

    private async getErrorEvents() {
        let errorEvents: IAppLogEntryModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                errorEvents = await this.hubApi.App.GetMostRecentErrorEvents(10);
            }
        );
        return errorEvents;
    }
}