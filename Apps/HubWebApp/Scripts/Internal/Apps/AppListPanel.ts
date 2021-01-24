import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppListCard } from "./AppListCard";
import { AppListPanelViewModel } from "./AppListPanelViewModel";

export class AppListPanel {
    public static readonly ResultKeys = {
        appSelected: 'app-selected'
    }

    constructor(
        private readonly vm: AppListPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            new Result(AppListPanel.ResultKeys.appSelected, app)
        );
    }

    private readonly appListCard = new AppListCard(this.vm.appListCard, this.hubApi);

    refresh() {
        return this.appListCard.refresh();
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }


}