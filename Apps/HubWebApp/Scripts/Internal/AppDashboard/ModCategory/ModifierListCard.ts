import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ListCard } from "../ListCard";
import { ListCardViewModel } from "../ListCardViewModel";
import { ModifierListItemViewModel } from "./ModifierListItemViewModel";

export class ModifierListCard extends ListCard {
    constructor(
        vm: ListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Modifiers were Found');
        vm.title('Modifiers');
    }

    private modCategoryID: number;

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    protected createItem(sourceItem: IModifierModel) {
        let item = new ModifierListItemViewModel();
        item.modKey(sourceItem.ModKey);
        item.displayText(sourceItem.DisplayText);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.ModCategory.GetModifiers(this.modCategoryID);
    }
}