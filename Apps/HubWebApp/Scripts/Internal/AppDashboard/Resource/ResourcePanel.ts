import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { Command } from "XtiShared/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourcePanelViewModel } from "./ResourcePanelViewModel";
import { ResourceComponent } from "./ResourceComponent";
import { ResourceAccessCard } from "./ResourceAccessCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";

export class ResourcePanel {
    public static readonly ResultKeys = {
        backRequested: 'back-requested'
    };

    constructor(
        private readonly vm: ResourcePanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('Resource Group');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
    }

    private readonly resourceComponent = new ResourceComponent(
        this.vm.resourceComponent,
        this.hubApi
    );
    private readonly resourceAccessCard = new ResourceAccessCard(
        this.vm.resourceAccessCard,
        this.hubApi
    );
    private readonly mostRecentRequestListCard = new MostRecentRequestListCard(
        this.vm.mostRecentRequestListCard,
        this.hubApi
    );
    private readonly mostRecentErrorEventListCard = new MostRecentErrorEventListCard(
        this.vm.mostRecentErrorEventListCard,
        this.hubApi
    );

    setResourceID(resourceID: number) {
        this.resourceComponent.setResourceID(resourceID);
        this.resourceAccessCard.setResourceID(resourceID);
        this.mostRecentRequestListCard.setResourceID(resourceID);
        this.mostRecentErrorEventListCard.setResourceID(resourceID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.resourceComponent.refresh(),
            this.resourceAccessCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(promises);
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    readonly backCommand = new Command(this.vm.backCommand, this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ResourcePanel.ResultKeys.backRequested));
    }
}