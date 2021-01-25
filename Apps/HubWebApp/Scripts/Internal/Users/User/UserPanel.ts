import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { AppListCard } from "../../Apps/AppListCard";
import { UserComponent } from "./UserComponent";
import { UserPanelViewModel } from "./UserPanelViewModel";

export class UserPanel {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        appSelected: 'app-selected'
    };

    constructor(
        private readonly vm: UserPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('Users');
        this.backCommand.makeLight();
        this.appListCard.setTitle('App Permissions');
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }

    private onAppSelected(app: IAppModel) {
        this.awaitable.resolve(
            new Result(UserPanel.ResultKeys.appSelected, app)
        );
    }

    private readonly userComponent = new UserComponent(this.vm.userComponent, this.hubApi);
    private readonly appListCard = new AppListCard(this.vm.appListCard, this.hubApi);

    setUserID(userID: number) {
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

    private readonly backCommand = new Command(this.vm.backCommand, this.back.bind(this));

    private back() {
        this.awaitable.resolve(
            new Result(UserPanel.ResultKeys.backRequested)
        );
    }
}