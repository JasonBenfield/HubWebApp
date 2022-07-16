import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { RoleAccessListItem } from "../RoleAccessListItem";
import { RoleAccessListItemView } from "../RoleAccessListItemView";

export class ResourceAccessCard {
    private readonly alert: MessageAlert;
    private readonly accessItems: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        view: ResourceAccessCardView
    ) {
        new TextComponent(view.titleHeader).setText('Permissions');
        this.alert = new CardAlert(view.alert).alert;
        this.accessItems = new ListGroup(view.accessItems);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
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
        let roles: IAppRoleModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                roles = await this.hubApi.Resource.GetRoleAccess({
                    VersionKey: 'Current',
                    ResourceID: this.resourceID
                });
            }
        );
        const accessItems: IRoleAccessItem[] = [];
        for (let allowedRole of roles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}