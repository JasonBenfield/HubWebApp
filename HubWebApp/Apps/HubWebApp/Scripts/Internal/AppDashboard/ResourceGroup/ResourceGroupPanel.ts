import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { ResourceGroupAccessCard } from "./ResourceGroupAccessCard";
import { ResourceGroupComponent } from "./ResourceGroupComponent";
import { ResourceGroupPanelView } from "./ResourceGroupPanelView";
import { ResourceListCard } from "./ResourceListCard";

interface Results {
    backRequested?: {};
    resourceSelected?: { resource: IResourceModel; };
    modCategorySelected?: { modCategory: IModifierCategoryModel };
}

export class ResourceGroupPanelResult {
    static get backRequested() { return new ResourceGroupPanelResult({ backRequested: {} }); }

    static resourceSelected(resource: IResourceModel) {
        return new ResourceGroupPanelResult({
            resourceSelected: { resource: resource }
        });
    }

    static modCategorySelected(modCategory: IModifierCategoryModel) {
        return new ResourceGroupPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: Results) {
    }

    get backRequested() { return this.results.backRequested; }

    get resourceSelected() { return this.results.resourceSelected; }

    get modCategorySelected() { return this.results.modCategorySelected; }
}

export class ResourceGroupPanel implements IPanel {
    private readonly resourceGroupComponent: ResourceGroupComponent;
    private readonly modCategoryComponent: ModCategoryComponent;
    private readonly roleAccessCard: ResourceGroupAccessCard;
    private readonly resourceListCard: ResourceListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;
    private readonly backCommand = new Command(this.back.bind(this));
    private readonly awaitable = new Awaitable<ResourceGroupPanelResult>();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ResourceGroupPanelView
    ) {
        this.resourceGroupComponent = new ResourceGroupComponent(this.hubApi, this.view.resourceGroupComponent);
        this.modCategoryComponent = new ModCategoryComponent(this.hubApi, this.view.modCategoryComponent);
        this.modCategoryComponent.clicked.register(
            this.onModCategoryClicked.bind(this)
        );
        this.roleAccessCard = new ResourceGroupAccessCard(this.hubApi, this.view.roleAccessCard);
        this.resourceListCard = new ResourceListCard(this.hubApi, this.view.resourceListCard);
        this.resourceListCard.resourceSelected.register(this.onResourceSelected.bind(this));
        this.mostRecentRequestListCard = new MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);
        this.backCommand.add(this.view.backButton);
    }

    private onModCategoryClicked(modCategory: IModifierCategoryModel) {
        this.awaitable.resolve(
            ResourceGroupPanelResult.modCategorySelected(modCategory)
        );
    }

    private onResourceSelected(resource: IResourceModel) {
        this.awaitable.resolve(ResourceGroupPanelResult.resourceSelected(resource));
    }

    setGroupID(groupID: number) {
        this.resourceGroupComponent.setGroupID(groupID);
        this.modCategoryComponent.setGroupID(groupID);
        this.roleAccessCard.setGroupID(groupID);
        this.resourceListCard.setGroupID(groupID);
        this.mostRecentRequestListCard.setGroupID(groupID);
        this.mostRecentErrorEventListCard.setGroupID(groupID);
    }

    async refresh() {
        let tasks: Promise<any>[] = [
            this.resourceGroupComponent.refresh(),
            this.modCategoryComponent.refresh(),
            this.roleAccessCard.refresh(),
            this.resourceListCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(tasks);
    }

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(ResourceGroupPanelResult.backRequested);
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}