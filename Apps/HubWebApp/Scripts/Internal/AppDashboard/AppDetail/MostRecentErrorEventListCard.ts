import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EventListItem } from "../EventListItem";
import { EventListItemViewModel } from "../EventListItemViewModel";
import { ListCard } from "../ListCard";
import { ListCardViewModel } from "../ListCardViewModel";

export class MostRecentErrorEventListCard extends ListCard {
    constructor(
        vm: ListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Errors were Found');
        vm.title('Most Recent Errors');
    }

    protected createItem(evt: IAppEventModel) {
        let item = new EventListItemViewModel();
        new EventListItem(item, evt);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.App.GetMostRecentErrorEvents(10);
    }
}