﻿import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";
import { MostRecentRequestListCardView } from "./MostRecentRequestListCardView";
import { ExpandedAppRequest } from "../../../Lib/ExpandedAppRequest";

export class MostRecentRequestListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup<RequestExpandedListItem, RequestExpandedListItemView>;

    constructor(
        private readonly hubApi: HubAppClient,
        view: MostRecentRequestListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Most Recent Requests');
        this.alert = new CardAlert(view.alert).alert;
        this.requests = new ListGroup(view.requests);
    }

    async refresh() {
        const sourceRequests = await this.getRequests();
        const requests = sourceRequests.map(r => new ExpandedAppRequest(r));
        this.requests.setItems(
            requests,
            (sourceItem, listItem) => new RequestExpandedListItem(sourceItem, listItem)
        );
        if (requests.length === 0) {
            this.alert.danger('No Requests were Found');
        }
    }

    private getRequests() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.App.GetMostRecentRequests(10)
        );
    }
}