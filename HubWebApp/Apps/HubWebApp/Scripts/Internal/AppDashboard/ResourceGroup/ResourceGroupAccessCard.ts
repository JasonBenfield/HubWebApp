import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
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
        new TextComponent(view.titleHeader).setText('Allowed Roles');
        this.alert = new CardAlert(view.alert).alert;
        this.accessItems = new ListGroup(view.accessItems);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
    }

    async refresh() {
        const accessItems = await this.getRoleAccessItems();
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
        const accessItems: IRoleAccessItem[] = [];
        for (let allowedRole of allowedRoles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}