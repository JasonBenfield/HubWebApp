import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { RequestExpandedListItem } from "../RequestExpandedListItem";

export class MostRecentRequestListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Most Recent Requests');
        this.alert = this.addCardAlert().alert;
        this.requests = this.addButtonListGroup();
    }

    private readonly alert: MessageAlert;
    private readonly requests: CardButtonListGroup;

    async refresh() {
        let requests = await this.getRequests();
        this.requests.setItems(
            requests,
            (sourceItem, listItem) => {
                listItem.addContent(new RequestExpandedListItem(sourceItem));
            }
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