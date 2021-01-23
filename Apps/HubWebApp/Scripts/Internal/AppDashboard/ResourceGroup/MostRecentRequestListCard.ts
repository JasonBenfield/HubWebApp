import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ListCard } from "../ListCard";
import { ListCardViewModel } from "../ListCardViewModel";
import { RequestExpandedListItem } from "../RequestExpandedListItem";
import { RequestExpandedListItemViewModel } from "../RequestExpandedListItemViewModel";

export class MostRecentRequestListCard extends ListCard {
    constructor(
        vm: ListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Requests were Found');
        vm.title('Most Recent Requests');
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    protected createItem(request: IAppRequestExpandedModel) {
        let item = new RequestExpandedListItemViewModel();
        new RequestExpandedListItem(item, request);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.ResourceGroup.GetMostRecentRequests(
            { GroupID: this.groupID, HowMany: 10 }
        );
    }
}