import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { ResourceGroupAccessCard } from "./ResourceGroupAccessCard";
import { ResourceGroupComponent } from "./ResourceGroupComponent";
import { ResourceGroupPanelView } from "./ResourceGroupPanelView";
import { ResourceListCard } from "./ResourceListCard";
import { AppResource } from "../../../Lib/AppResource";
import { ModifierCategory } from "../../../Lib/ModifierCategory";

interface IResult {
    backRequested?: {};
    resourceSelected?: { resource: AppResource; };
    modCategorySelected?: { modCategory: ModifierCategory };
}

class Result {
    static backRequested() { return new Result({ backRequested: {} }); }

    static resourceSelected(resource: AppResource) {
        return new Result({
            resourceSelected: { resource: resource }
        });
    }

    static modCategorySelected(modCategory: ModifierCategory) {
        return new Result({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: IResult) {
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
    private readonly awaitable = new Awaitable<Result>();

    constructor(hubClient: HubAppClient, private readonly view: ResourceGroupPanelView) {
        this.resourceGroupComponent = new ResourceGroupComponent(hubClient, view.resourceGroupComponent);
        this.modCategoryComponent = new ModCategoryComponent(hubClient, view.modCategoryComponent);
        this.modCategoryComponent.when.clicked.then(
            this.onModCategoryClicked.bind(this)
        );
        this.roleAccessCard = new ResourceGroupAccessCard(hubClient, view.roleAccessCard);
        this.resourceListCard = new ResourceListCard(hubClient, view.resourceListCard);
        this.resourceListCard.when.resourceSelected.then(this.onResourceSelected.bind(this));
        this.mostRecentRequestListCard = new MostRecentRequestListCard(hubClient, view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(hubClient, view.mostRecentErrorEventListCard);
        this.backCommand.add(view.backButton);
    }

    private onModCategoryClicked(modCategory: ModifierCategory) {
        this.awaitable.resolve(
            Result.modCategorySelected(modCategory)
        );
    }

    private onResourceSelected(resource: AppResource) {
        this.awaitable.resolve(Result.resourceSelected(resource));
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
        const tasks: Promise<any>[] = [
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
        this.awaitable.resolve(Result.backRequested());
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}