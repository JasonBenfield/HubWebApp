import { DefaultEvent } from "XtiShared/Events";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { SelectableListCard } from "../ListCard/SelectableListCard";
import { SelectableListCardViewModel } from "../ListCard/SelectableListCardViewModel";
import { UserListItemViewModel } from "./UserListItemViewModel";

export class UserListCard extends SelectableListCard {
    constructor(
        vm: SelectableListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Users were Found');
        vm.title('Users');
    }

    private readonly _userSelected = new DefaultEvent<IAppUserModel>(this);
    readonly userSelected = this._userSelected.handler();

    protected onItemSelected(item: UserListItemViewModel) {
        this._userSelected.invoke(item.source);
    }

    protected createItem(sourceItem: IAppUserModel) {
        let item = new UserListItemViewModel(sourceItem);
        item.userName(sourceItem.UserName);
        item.name(sourceItem.Name);
        return item;
    }

    protected getSourceItems() {
        return this.hubApi.Users.GetUsers();
    }
}