import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";

export class MostRecentRequestListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup<RequestExpandedListItem, RequestExpandedListItemView>;

    private groupID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: MostRecentRequestListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Most Recent Requests');
        this.alert = new CardAlert(view.alert).alert;
        this.requests = new ListGroup(view.requests);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const requests = await this.getRequests();
        this.requests.setItems(
            requests,
            (sourceItem, listItem) =>
                new RequestExpandedListItem(sourceItem, listItem)
        );
        if (requests.length === 0) {
            this.alert.danger('No Requests were Found');
        }
    }

    private getRequests() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ResourceGroup.GetMostRecentRequests({
                VersionKey: 'Current',
                GroupID: this.groupID,
                HowMany: 10
            })
        );
    }
}