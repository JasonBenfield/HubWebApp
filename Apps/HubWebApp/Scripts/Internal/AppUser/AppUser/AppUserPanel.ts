import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command/Command";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserComponent } from "./UserComponent";
import { UserRoleListCard } from "./UserRoleListCard";
import { HubTheme } from "../../HubTheme";
import { Card } from "XtiShared/Card/Card";
import { MarginCss } from "XtiShared/MarginCss";

export class AppUserPanel extends Block {
    static readonly ResultKeys = {
        backRequested: 'back-requested',
        editUserRolesRequested: 'edit-user-roles-requested'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userComponent = flexFill.addContent(new UserComponent(this.hubApi))
            .configure(c => c.setMargin(MarginCss.bottom(3)));
        this.userRoles = flexFill.addContent(new UserRoleListCard(this.hubApi));
        this.userRoles.editRequested.register(this.onEditUserRolesRequested.bind(this));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        ).configure(b => b.setText('User'));
    }

    private onEditUserRolesRequested() {
        this.awaitable.resolve(
            new Result(AppUserPanel.ResultKeys.editUserRolesRequested)
        );
    }

    private readonly userComponent: UserComponent;
    private readonly userRoles: UserRoleListCard;

    setUserID(userID: number) {
        this.userComponent.setUserID(userID);
        this.userRoles.setUserID(userID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.userComponent.refresh(),
            this.userRoles.refresh()
        ];
        return Promise.all(promises);
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly backCommand = new Command(this.back.bind(this));

    private back() {
        this.awaitable.resolve(
            new Result(AppUserPanel.ResultKeys.backRequested)
        );
    }
}