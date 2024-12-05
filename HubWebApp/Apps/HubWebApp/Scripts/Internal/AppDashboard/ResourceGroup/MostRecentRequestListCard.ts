import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { ExpandedAppRequest } from "../../../Lib/ExpandedAppRequest";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";

export class MostRecentRequestListCard {
    private readonly alert: IMessageAlert;
    private readonly requests: ListGroup<RequestExpandedListItem, RequestExpandedListItemView>;

    private groupID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: MostRecentRequestListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Most Recent Requests');
        this.alert = new CardAlert(view.alert);
        this.requests = new ListGroup(view.requests);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const sourceRequests = await this.getRequests();
        const requests = sourceRequests.map(r => new ExpandedAppRequest(r));
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