import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { AppRole } from "../../../Lib/AppRole";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { IRoleAccessItem } from "../../IRoleAccessItem";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { RoleAccessListItem } from "../RoleAccessListItem";
import { RoleAccessListItemView } from "../RoleAccessListItemView";

export class ResourceGroupAccessCard {
    private readonly alert: IMessageAlert;
    private readonly accessItems: ListGroup<RoleAccessListItem, RoleAccessListItemView>;

    private groupID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ResourceAccessCardView
    ) {
        new TextComponent(view.titleHeader).setText('Allowed Roles');
        this.alert = new CardAlert(view.alert);
        this.accessItems = new ListGroup(view.accessItems);
    }

    setGroupID(groupID: number) {
        this.groupID = groupID;
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
        const sourceAllowedRoles = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ResourceGroupInquiry.GetRoleAccess({
                VersionKey: 'Current',
                GroupID: this.groupID
            })
        );
        const allowedRoles = sourceAllowedRoles.map(r => new AppRole(r));
        const accessItems: IRoleAccessItem[] = [];
        for (const allowedRole of allowedRoles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}