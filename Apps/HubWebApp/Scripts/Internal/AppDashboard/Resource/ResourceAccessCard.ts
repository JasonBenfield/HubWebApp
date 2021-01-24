import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ListCard } from "../../ListCard/ListCard";
import { ListCardViewModel } from "../../ListCard/ListCardViewModel";
import { RoleAccessListItemViewModel } from "../RoleAccessListItemViewModel";

interface IRoleAccessItem {
    readonly isAllowed: boolean;
    readonly role: IAppRoleModel;
}

export class ResourceAccessCard extends ListCard {
    constructor(
        vm: ListCardViewModel,
        private readonly hubApi: HubAppApi
    ) {
        super(vm, 'No Roles were Found');
        vm.title('Permissions');
    }

    private resourceID: number;

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    protected createItem(sourceItem: IRoleAccessItem) {
        let item = new RoleAccessListItemViewModel();
        item.roleName(sourceItem.role.Name);
        item.isAllowed(sourceItem.isAllowed);
        return item;
    }

    protected async getSourceItems() {
        let access = await this.hubApi.Resource.GetRoleAccess(this.resourceID);
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