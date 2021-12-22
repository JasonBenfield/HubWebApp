import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";
import { MostRecentRequestListCardView } from "./MostRecentRequestListCardView";

export class MostRecentRequestListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: MostRecentRequestListCardView
    ) {
        new CardTitleHeader('Most Recent Requests', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.requests = new ListGroup(this.view.requests);
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
                requests = await this.hubApi.App.GetMostRecentRequests(10);
            }
        );
        return requests;
    }
}