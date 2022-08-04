﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { EventListItemView } from "../EventListItemView";
import { MostRecentErrorEventListCardView } from "../MostRecentErrorEventListCardView";

export class MostRecentErrorEventListCard {
    private readonly alert: MessageAlert;
    private readonly errorEvents: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
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
        const errorEvents = await this.getErrorEvents();
        this.errorEvents.setItems(
            errorEvents,
            (sourceItem: IAppLogEntryModel, listItem: EventListItemView) =>
                new EventListItem(sourceItem, listItem)
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