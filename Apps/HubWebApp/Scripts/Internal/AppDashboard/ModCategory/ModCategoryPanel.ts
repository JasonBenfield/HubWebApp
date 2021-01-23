import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponent } from "./ModCategoryComponent";
import { ModCategoryPanelViewModel } from "./ModCategoryPanelViewModel";
import { ModifierListCard } from "./ModifierListCard";
import { ResourceGroupListCard } from "./ResourceGroupListCard";

export class ModCategoryPanel {
    public static readonly ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected'
    };

    constructor(
        private readonly vm: ModCategoryPanelViewModel,
        private readonly hubApi: HubAppApi
    ) {
        let backIcon = this.backCommand.icon();
        backIcon.setName('fa-caret-left');
        this.backCommand.setText('App');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.resourceGroupListCard.resourceGroupSelected.register(
            this.onResourceGroupSelected.bind(this)
        );
    }

    private onResourceGroupSelected(resourceGroup: IResourceGroupModel) {
        this.awaitable.resolve(
            new Result(ModCategoryPanel.ResultKeys.resourceGroupSelected, resourceGroup)
        );
    }

    private readonly modCategoryComponent = new ModCategoryComponent(
        this.vm.modCategoryComponent,
        this.hubApi
    );
    private readonly modifierListCard = new ModifierListCard(
        this.vm.modifierListCard,
        this.hubApi
    );
    private readonly resourceGroupListCard = new ResourceGroupListCard(
        this.vm.resourceGroupListCard,
        this.hubApi
    );

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

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    readonly backCommand = new Command(this.vm.backCommand, this.back.bind(this));

    private back() {
        this.awaitable.resolve(new Result(ModCategoryPanel.ResultKeys.backRequested));
    }
}