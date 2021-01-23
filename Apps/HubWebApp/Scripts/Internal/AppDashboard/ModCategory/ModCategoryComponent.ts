import { Alert } from "XtiShared/Alert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponentViewModel } from "./ModCategoryComponentViewModel";

export class ModCategoryComponent {
    constructor(
        private readonly vm: ModCategoryComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    private modCategoryID: number;

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
        this.vm.name('');
    }

    private readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let modCategory = await this.getModCategory(this.modCategoryID);
        this.vm.name(modCategory.Name);
    }

    private async getModCategory(modCategoryID: number) {
        let modCategory: IModifierCategoryModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategory = await this.hubApi.ModCategory.GetModCategory(modCategoryID);
            }
        );
        return modCategory;
    }
}