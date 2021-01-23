import { DefaultEvent } from "XtiShared/Events";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { SelectableListCard } from "../SelectableListCard";
import { SelectableListCardViewModel } from "../SelectableListCardViewModel";
import { ResourceGroupListItemViewModel } from "../ResourceGroupListItemViewModel";

export class ResourceGroupListCard extends SelectableListCard {
    constructor(
        vm: SelectableListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Resource Groups were Found');
        vm.title('Resource Groups');
    }

    private readonly _resourceSelected = new DefaultEvent<IResourceGroupModel>(this);
    readonly resourceGroupSelected = this._resourceSelected.handler();

    protected onItemSelected(item: ResourceGroupListItemViewModel) {
        this._resourceSelected.invoke(item.source);
    }

    protected createItem(group: IResourceGroupModel) {
        let item = new ResourceGroupListItemViewModel(group);
        item.name(group.Name);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.App.GetResourceGroups();
    }
}