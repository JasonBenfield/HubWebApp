﻿import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { ModCategoryPanelView } from "./ModCategoryPanelView";
import { ModifierListCard } from "./ModifierListCard";
import { ResourceGroupListCard } from "./ResourceGroupListCard";
import { AppResourceGroup } from "../../../Lib/AppResourceGroup";

interface IResult {
    backRequested?: {};
    resourceGroupSelected?: { resourceGroup: AppResourceGroup };
}

class Result {
    static backRequested() {
        return new Result({ backRequested: {} });
    }

    static resourceGroupSelected(resourceGroup: AppResourceGroup) {
        return new Result({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    }

    private constructor(private readonly results: IResult) { }

    get backRequested() { return this.results.backRequested; }

    get resourceGroupSelected() { return this.results.resourceGroupSelected; }
}

export class ModCategoryPanel implements IPanel {
    private readonly modCategoryComponent: ModCategoryComponent;
    private readonly modifierListCard: ModifierListCard;
    private readonly resourceGroupListCard: ResourceGroupListCard;

    private readonly awaitable = new Awaitable<Result>();

    readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: ModCategoryPanelView
    ) {
        this.modCategoryComponent = new ModCategoryComponent(this.hubClient, this.view.modCategoryComponent);
        this.modifierListCard = new ModifierListCard(this.hubClient, this.view.modifierListCard);
        this.resourceGroupListCard = new ResourceGroupListCard(this.hubClient, this.view.resourceGroupListCard);
        this.backCommand.add(this.view.backButton);
        this.resourceGroupListCard.when.resourceGroupSelected.then(
            this.onResourceGroupSelected.bind(this)
        );
    }

    private onResourceGroupSelected(resourceGroup: AppResourceGroup) {
        this.awaitable.resolve(
            Result.resourceGroupSelected(resourceGroup)
        );
    }

    setModCategoryID(categoryID: number) {
        this.modCategoryComponent.setModCategoryID(categoryID);
        this.modifierListCard.setModCategoryID(categoryID);
        this.resourceGroupListCard.setModCategoryID(categoryID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.modCategoryComponent.refresh(),
            this.modifierListCard.refresh(),
            this.resourceGroupListCard.refresh()
        ];
        return Promise.all(promises);
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