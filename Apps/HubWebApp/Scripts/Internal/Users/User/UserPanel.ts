import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command/Command";
import { Result } from "XtiShared/Result";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { MarginCss } from "XtiShared/MarginCss";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { AppListCard } from "../../Apps/AppListCard";
import { UserComponent } from "./UserComponent";
import { HubTheme } from "../../HubTheme";

export class UserPanel extends Block {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        appSelected: 'app-selected',
        editRequested: 'edit-requested'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        this.setName(UserPanel.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.userComponent = flexFill.container.addContent(new UserComponent(this.hubApi))
            .configure(c => c.setMargin(MarginCss.bottom(3)));
        this.appListCard = flexFill.container.addContent(
            new AppListCard(
                this.hubApi,
                appID => this.hubApi.UserInquiry.RedirectToAppUser.getUrl({
                    AppID: appID,
                    UserID: this.userID
                }).toString()
            )
        ).configure(c => c.setMargin(MarginCss.bottom(3)));
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backCommand.add(
            toolbar.columnStart.addContent(HubTheme.instance.commandToolbar.backButton())
        ).configure(b => b.setText('App Permissions'));
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
        this.userComponent.editRequested.register(this.onEditRequested.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            new Result(UserPanel.ResultKeys.appSelected, app)
        );
    }

    private onEditRequested(userID: number) {
        this.awaitable.resolve(
            new Result(UserPanel.ResultKeys.editRequested, userID)
        );
    }

    private readonly userComponent: UserComponent;
    private readonly appListCard: AppListCard;

    private userID: number; 

    setUserID(userID: number) {
        this.userID = userID;
        this.userComponent.setUserID(userID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.userComponent.refresh(),
            this.appListCard.refresh()
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
            new Result(UserPanel.ResultKeys.backRequested)
        );
    }
}