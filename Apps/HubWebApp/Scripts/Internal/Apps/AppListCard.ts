import { DefaultEvent } from "../../../Imports/Shared/Events";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { SelectableListCard } from "../ListCard/SelectableListCard";
import { SelectableListCardViewModel } from "../ListCard/SelectableListCardViewModel";
import { AppListItemViewModel } from "./AppListItemViewModel";

export class AppListCard extends SelectableListCard {
    constructor(
        vm: SelectableListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Apps were Found');
        vm.componentName('selectable-list-card');
        vm.title('Apps');
    }

    private readonly _appSelected = new DefaultEvent<IAppModel>(this);
    readonly appSelected = this._appSelected.handler();

    setTitle(title: string) {
        this.vm.title(title);
    }

    protected onItemSelected(item: AppListItemViewModel) {
        this._appSelected.invoke(item.source);
    }

    protected createItem(sourceItem: IAppModel) {
        let item = new AppListItemViewModel(sourceItem);
        item.appName(sourceItem.AppName);
        item.title(sourceItem.Title);
        item.type(sourceItem.Type.DisplayText);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.Apps.All();
    }
}