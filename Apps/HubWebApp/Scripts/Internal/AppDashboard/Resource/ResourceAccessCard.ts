/// <reference path="../../index.d.ts" />
import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceAccessCardView } from "../ResourceAccessCardView";
import { RoleAccessListItem } from "../RoleAccessListItem";
import { RoleAccessListItemView } from "../RoleAccessListItemView";

export class ResourceAccessCard {
    private readonly alert: MessageAlert;
    private readonly accessItems: ListGroup;

    private resourceID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceAccessCardView
    ) {
        new TextBlock('Permissions', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.accessItems = new ListGroup(this.view.accessItems);
    }

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
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
        let accessItems: IRoleAccessItem[] = [];
        for (let allowedRole of roles) {
            accessItems.push({
                isAllowed: true,
                role: allowedRole
            });
        }
        return accessItems;
    }
}