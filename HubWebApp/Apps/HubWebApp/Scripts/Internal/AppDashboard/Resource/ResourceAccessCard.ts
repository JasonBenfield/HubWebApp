import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { RoleAccessListItem } from "../RoleAccessListItem";
import { RoleAccessListItemView } from "../RoleAccessListItemView";
import { IRoleAccessItem } from "../../IRoleAccessItem";
import { AppRole } from "../../../Lib/AppRole";

export class ResourceAccessCard {
    private readonly alert: MessageAlert;
    private readonly accessItems: ListGroup<RoleAccessListItem, RoleAccessListItemView>;

    private resourceID: number;

    constructor(
        private readonly hubClient: HubAppClient,
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
            (sourceItem, listItem) =>
                new RoleAccessListItem(sourceItem, listItem)
        );
        if (accessItems.length === 0) {
            this.alert.danger('No Roles were Found');
        }
    }

    private async getRoleAccessItems() {
        const sourceRoles = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Resource.GetRoleAccess({
                VersionKey: 'Current',
                ResourceID: this.resourceID
            })
        );
        const roles = sourceRoles.map(r => new AppRole(r));
        const accessItems: IRoleAccessItem[] = [];
        for (const allowedRole of roles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}