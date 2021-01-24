import { DefaultEvent } from "XtiShared/Events";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceResultType } from "../../../Hub/Api/ResourceResultType";
import { SelectableListCard } from "../../ListCard/SelectableListCard";
import { SelectableListCardViewModel } from "../../ListCard/SelectableListCardViewModel";
import { ResourceListItemViewModel } from "./ResourceListItemViewModel";

export class ResourceListCard extends SelectableListCard {
    constructor(
        vm: SelectableListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Resources were Found');
        vm.title('Resources');
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    private readonly _resourceSelected = new DefaultEvent<IResourceModel>(this);
    readonly resourceSelected = this._resourceSelected.handler();

    protected onItemSelected(item: ResourceListItemViewModel) {
        this._resourceSelected.invoke(item.source);
    }

    protected createItem(sourceItem: IResourceModel) {
        let item = new ResourceListItemViewModel(sourceItem);
        item.name(sourceItem.Name);
        let resultType = ResourceResultType.values.value(sourceItem.ResultType.Value);
        let resultTypeText: string;
        if (
            resultType.equalsAny(ResourceResultType.values.None, ResourceResultType.values.Json)
        ) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resultType.DisplayText;
        }
        item.resultType(resultTypeText);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.ResourceGroup.GetResources(this.groupID);
    }
}