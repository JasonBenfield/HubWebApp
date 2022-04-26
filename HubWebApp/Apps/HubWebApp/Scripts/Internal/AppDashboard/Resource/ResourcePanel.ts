import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { ResourceAccessCard } from "./ResourceAccessCard";
import { ResourceComponent } from "./ResourceComponent";
import { ResourcePanelView } from "./ResourcePanelView";

interface Results {
    backRequested: {};
}

export class ResourcePanelResult {
    static get backRequested() { return new ResourcePanelResult({ backRequested: {} }); }

    private constructor(private readonly results: Results) {
    }

    get backRequested() { return this.results.backRequested; }
}

export class ResourcePanel implements IPanel {
    private readonly resourceComponent: ResourceComponent;
    private readonly resourceAccessCard: ResourceAccessCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;
    private readonly awaitable = new Awaitable<ResourcePanelResult>();
    readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourcePanelView
    ) {
        this.resourceComponent = new ResourceComponent(this.hubApi, this.view.resourceComponent);
        this.resourceAccessCard = new ResourceAccessCard(this.hubApi, this.view.resourceAccessCard);
        this.mostRecentRequestListCard = new MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);
        this.backCommand.add(this.view.backButton);
    }

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

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(ResourcePanelResult.backRequested);
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}