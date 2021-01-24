import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ListCard } from "../../ListCard/ListCard";
import { ListCardViewModel } from "../../ListCard/ListCardViewModel";
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

    protected createItem(request: IAppRequestExpandedModel) {
        let item = new RequestExpandedListItemViewModel();
        new RequestExpandedListItem(item, request);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.App.GetMostRecentRequests(10);
    }
}