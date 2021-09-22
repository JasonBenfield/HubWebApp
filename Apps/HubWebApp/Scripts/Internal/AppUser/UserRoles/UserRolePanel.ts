import { Card } from "XtiShared/Card/Card";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { ButtonListGroup } from "XtiShared/ListGroup/ButtonListGroup";
import { ButtonListItemViewModel } from "XtiShared/ListGroup/ButtonListItemViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command/Command";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserRoleListItem } from "./EditUserRoleListItem";
import { HubTheme } from "../../HubTheme";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";

export class UserRolePanel extends Block {
    public static ResultKeys = {
        backRequested: 'back-requested'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        let card = flexFill.addContent(new Card());
        card.addCardTitleHeader('Edit User Roles');
        this.alert = card.addCardAlert().alert;
        this.userRoles = card.addButtonListGroup(
            (itemVM: ButtonListItemViewModel) => new EditUserRoleListItem(this.hubApi, itemVM)
        );
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        );
    }

    private readonly alert: MessageAlert;
    private readonly userRoles: ButtonListGroup;

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let access = await this.getUserRoleAccess(this.userID);
        let sourceItems: IAppRoleModel[] = [];
        for (let userRole of access.AssignedRoles) {
            sourceItems.push(userRole);
        }
        for (let role of access.UnassignedRoles) {
            sourceItems.push(role);
        }
        sourceItems.sort(this.compare.bind(this));
        this.userRoles.setItems(
            sourceItems,
            (sourceItem, listItem: EditUserRoleListItem) => {
                listItem.setUserID(this.userID);
                listItem.withAssignedRole(sourceItem);
            }
        );
    }

    private compare(a: IAppRoleModel, b: IAppRoleModel) {
        let roleName: string;
        roleName = a.Name;
        let otherRoleName: string;
        otherRoleName = b.Name;
        let result: number;
        if (roleName < otherRoleName) {
            result = -1;
        }
        else if (roleName === otherRoleName) {
            result = 0;
        }
        else {
            result = 1;
        }
        return result;
    }

    private async getUserRoleAccess(userID: number) {
        let access: IUserRoleAccessModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                let request: IGetUserRoleAccessRequest = {
                    UserID: userID,
                    ModifierID: 0
                };
                access = await this.hubApi.AppUser.GetUserRoleAccess(request);
            }
        );
        return access;
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(
            new Result(UserRolePanel.ResultKeys.backRequested)
        );
    }
}