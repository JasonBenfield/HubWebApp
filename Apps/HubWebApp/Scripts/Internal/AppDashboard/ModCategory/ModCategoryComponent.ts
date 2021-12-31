import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private modCategoryID: number;
    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextBlock;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModCategoryComponentView
    ) {
        new TextBlock('Modifier Category', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.modCategoryName = new TextBlock('', this.view.modCategoryName);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        let modCategory = await this.getModCategory(this.modCategoryID);
        this.modCategoryName.setText(modCategory.Name);
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