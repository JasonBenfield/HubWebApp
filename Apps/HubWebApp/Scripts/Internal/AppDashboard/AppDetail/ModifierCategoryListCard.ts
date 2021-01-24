import { DefaultEvent } from "XtiShared/Events";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { SelectableListCard } from "../../ListCard/SelectableListCard";
import { SelectableListCardViewModel } from "../../ListCard/SelectableListCardViewModel";
import { ModifierCategoryListItemViewModel } from "./ModifierCategoryListItemViewModel";

export class ModifierCategoryListCard extends SelectableListCard {
    constructor(
        vm: SelectableListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Modifier Categories were Found');
        vm.title('Modifier Categories');
    }

    private readonly _modCategorySelected = new DefaultEvent<IModifierCategoryModel>(this);
    readonly modCategorySelected = this._modCategorySelected.handler();

    protected onItemSelected(item: ModifierCategoryListItemViewModel) {
        this._modCategorySelected.invoke(item.source);
    }

    protected createItem(r: IModifierCategoryModel) {
        let item = new ModifierCategoryListItemViewModel(r);
        item.name(r.Name);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.App.GetModifierCategories();
    }
}