import { ListCard } from "./ListCard";
import { SelectableListCardViewModel } from "./SelectableListCardViewModel";

export abstract class SelectableListCard extends ListCard {
    constructor(
        vm: SelectableListCardViewModel,
        noItemsFoundMessage
    ) {
        super(vm, noItemsFoundMessage);
        vm.itemSelected.register(this.onItemSelected.bind(this));
    }

    protected abstract onItemSelected(item: any);
}