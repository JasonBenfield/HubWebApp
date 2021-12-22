import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private modCategoryID: number;
    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModCategoryComponentView
    ) {
        new CardTitleHeader('Modifier Category', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        let modCategory = await this.getModCategory(this.modCategoryID);
        this.view.setModCategoryName(modCategory.Name);
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