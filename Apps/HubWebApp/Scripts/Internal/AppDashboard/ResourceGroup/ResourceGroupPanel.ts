import { Awaitable } from "XtiShared/Awaitable";
import { Result } from "XtiShared/Result";
import { Command } from "XtiShared/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ResourceGroupAccessCard } from "./ResourceGroupAccessCard";
import { ResourceGroupComponent } from "./ResourceGroupComponent";
import { ResourceGroupPanelViewModel } from "./ResourceGroupPanelViewModel";
import { ResourceListCard } from "./ResourceListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { ModCategoryComponent } from "./ModCategoryComponent";

export class ResourceGroupPanel {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceSelected: 'resource-selected',
        modCategorySelected: 'mod-category-selected'
    };

    constructor(
        private readonly vm: ResourceGroupPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('App');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.modCategoryComponent.clicked.register(
            this.onModCategoryClicked.bind(this)
        );
        this.resourceListCard.resourceSelected.register(this.onResourceSelected.bind(this));
    }

    private onModCategoryClicked(modCategory: IModifierCategoryModel) {
        this.awaitable.resolve(
            new Result(ResourceGroupPanel.ResultKeys.modCategorySelected, modCategory)
        );
    }

    private onResourceSelected(resource: IResourceModel) {
        this.awaitable.resolve(new Result(ResourceGroupPanel.ResultKeys.resourceSelected, resource));
    }

    setGroupID(groupID: number) {
        this.resourceGroupComponent.setGroupID(groupID);
        this.modCategoryComponent.setGroupID(groupID);
        this.roleAccessCard.setGroupID(groupID);
        this.resourceListCard.setGroupID(groupID);
        this.mostRecentRequestListCard.setGroupID(groupID);
        this.mostRecentErrorEventListCard.setGroupID(groupID);
    }

    private readonly resourceGroupComponent = new ResourceGroupComponent(
        this.vm.resourceGroupComponent,
        this.hubApi
    );
    private readonly modCategoryComponent = new ModCategoryComponent(
        this.vm.modCategoryComponent,
        this.hubApi
    );
    private readonly roleAccessCard = new ResourceGroupAccessCard(
        this.vm.roleAccessCard,
        this.hubApi
    );
    private readonly resourceListCard = new ResourceListCard(
        this.vm.resourceListCard,
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

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly backCommand = new Command(this.vm.backCommand, this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ResourceGroupPanel.ResultKeys.backRequested));
    }
}