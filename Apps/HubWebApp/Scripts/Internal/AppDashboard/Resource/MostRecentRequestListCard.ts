import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";

export class MostRecentRequestListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: MostRecentRequestListCardView
    ) {
        new TextBlock('Most Recent Requests', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.requests = new ListGroup(this.view.requests);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        let requests = await this.getRequests();
        this.requests.setItems(
            requests,
            (sourceItem: IAppRequestExpandedModel, listItem: RequestExpandedListItemView) =>
                new RequestExpandedListItem(sourceItem, listItem)
        );
        if (requests.length === 0) {
            this.alert.danger('No Requests were Found');
        }
    }

    private async getRequests() {
        let requests: IAppRequestExpandedModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                requests = await this.hubApi.Resource.GetMostRecentRequests({
                    VersionKey: 'Current',
                    ResourceID: this.resourceID,
                    HowMany: 10
                });
            }
        );
        return requests;
    }
}