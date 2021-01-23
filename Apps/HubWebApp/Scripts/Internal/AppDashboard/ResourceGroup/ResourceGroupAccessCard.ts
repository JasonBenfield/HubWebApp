import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ListCard } from "../ListCard";
import { ListCardViewModel } from "../ListCardViewModel";
import { RoleAccessListItemViewModel } from "../RoleAccessListItemViewModel";

interface IRoleAccessItem {
    readonly isAllowed: boolean;
    readonly role: IAppRoleModel;
}

export class ResourceGroupAccessCard extends ListCard {
    constructor(
        vm: ListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Roles were Found');
        vm.title('Permissions');
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    protected createItem(sourceItem: IRoleAccessItem) {
        let item = new RoleAccessListItemViewModel();
        item.roleName(sourceItem.role.Name);
        item.isAllowed(sourceItem.isAllowed);
        return item;
    }

    protected async getSourceItems() {
        let access = await this.hubApi.ResourceGroup.GetRoleAccess(this.groupID);
        let accessItems: IRoleAccessItem[] = [];
        for (let allowedRole of access.AllowedRoles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        for (let deniedRole of access.DeniedRoles) {
            accessItems.push({
                isAllowed: false,
                role: deniedRole
            });
        }
        return accessItems;
    }
}