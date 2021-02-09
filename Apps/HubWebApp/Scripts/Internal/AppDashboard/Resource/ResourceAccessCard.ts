﻿import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { RoleAccessListItem } from "../RoleAccessListItem";

export class ResourceAccessCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('Permissions');
        this.alert = this.addCardAlert().alert;
    }

    private readonly alert: MessageAlert;
    private readonly accessItems: CardButtonListGroup;

    private resourceID: number;

    setResourceID(resourceID: number) {
        this.resourceID = resourceID;
    }

    async refresh() {
        let accessItems = await this.getRoleAccessItems();
        this.accessItems.setItems(
            accessItems,
            (sourceItem, listItem) => {
                listItem.addContent(new RoleAccessListItem(sourceItem));
            }
        );
        if (accessItems.length === 0) {
            this.alert.danger('No Roles were Found');
        }
    }

    private async getRoleAccessItems() {
        let access: IRoleAccessModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                access = await this.hubApi.Resource.GetRoleAccess(this.resourceID);
            }
        );
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