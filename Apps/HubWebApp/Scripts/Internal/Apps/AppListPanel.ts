import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { Block } from "XtiShared/Html/Block";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppListCard } from "./AppListCard";

export class AppListPanel extends Block {
    public static readonly ResultKeys = {
        appSelected: 'app-selected'
    }

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        this.appListCard = flexColumn.addContent(new FlexColumnFill())
            .addContent(
                new AppListCard(
                    this.hubApi,
                    appID => this.hubApi.Apps.RedirectToApp.getUrl(appID).toString()
                )
            );
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            new Result(AppListPanel.ResultKeys.appSelected, app)
        );
    }

    private readonly appListCard: AppListCard;

    refresh() {
        return this.appListCard.refresh();
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }


}