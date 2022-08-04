import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { MostRecentRequestListCardView } from "../MostRecentRequestListCardView";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";

export class MostRecentRequestListCard {
    private readonly alert: MessageAlert;
    private readonly requests: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        view: MostRecentRequestListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Most Recent Requests');
        this.alert = new CardAlert(view.alert).alert;
        this.requests = new ListGroup(view.requests);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        const requests = await this.getRequests();
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