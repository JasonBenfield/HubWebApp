import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { RoleAccessListItem } from "../RoleAccessListItem";
import { RoleAccessListItemView } from "../RoleAccessListItemView";

export class ResourceGroupAccessCard {
    private readonly alert: MessageAlert;
    private readonly accessItems: ListGroup;

    private groupID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceAccessCardView
    ) {
        new CardTitleHeader('Permissions', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        let accessItems = await this.getRoleAccessItems();
        this.accessItems.setItems(
            accessItems,
            (sourceItem: IRoleAccessItem, listItem: RoleAccessListItemView) =>
                new RoleAccessListItem(sourceItem, listItem)
        );
        if (accessItems.length === 0) {
            this.alert.danger('No Roles were Found');
        }
    }

    private async getRoleAccessItems() {
        let allowedRoles: IAppRoleModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                allowedRoles = await this.hubApi.ResourceGroup.GetRoleAccess({
                    VersionKey: 'Current',
                    GroupID: this.groupID
                });
            }
        );
        let accessItems: IRoleAccessItem[] = [];
        for (let allowedRole of allowedRoles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}