import { Alert } from "XtiShared/Alert";
import { DefaultEvent } from "XtiShared/Events";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponentViewModel } from "./ModCategoryComponentViewModel";

export class ModCategoryComponent {
    constructor(
        private readonly vm: ModCategoryComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.vm.clicked.register(this.onClicked.bind(this));
    }

    private readonly _clicked = new DefaultEvent<IModifierCategoryModel>(this);
    readonly clicked = this._clicked.handler();

    private onClicked() {
        this._clicked.invoke(this.modCategory);
    }

    private groupID: number;

    setGroupID(groupID: number) {
        this.groupID = groupID;
        this.vm.name('');
    }

    private readonly alert = new Alert(this.vm.alert);

    private modCategory: IModifierCategoryModel;

    async refresh() {
        let modCategory = await this.getModCategory(this.groupID);
        this.vm.name(modCategory.Name);
        this.modCategory = modCategory;
    }

    private async getModCategory(groupID: number) {
        let modCategory: IModifierCategoryModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategory = await this.hubApi.ResourceGroup.GetModCategory(groupID);
            }
        );
        return modCategory;
    }
}