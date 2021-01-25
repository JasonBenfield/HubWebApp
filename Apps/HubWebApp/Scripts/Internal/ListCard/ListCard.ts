import { Alert } from "XtiShared/Alert";
import { MappedArray } from "XtiShared/Enumerable";
import { ListCardViewModel } from "./ListCardViewModel";

export abstract class ListCard {
    constructor(
        protected readonly vm: ListCardViewModel,
        private readonly noItemsFoundMessage
    ) {
    }


    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let sourceItems = await this._getSourceItems();
        let items = new MappedArray(
            sourceItems,
            s => { return this.createItem(s); }
        ).value();
        this.vm.items(items);
        this.vm.hasItems(sourceItems.length > 0);
        if (sourceItems.length === 0) {
            this.alert.danger(this.noItemsFoundMessage);
        }
    }

    protected abstract createItem(sourceItem: any): any;

    private async _getSourceItems() {
        let sourceItems: any[] = [];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                sourceItems = await this.getSourceItems();
            }
        );
        return sourceItems;
    }

    protected abstract getSourceItems(): Promise<any>;
}