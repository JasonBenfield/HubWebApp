import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private modCategoryID: number;
    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextComponent;

    constructor(
        private readonly hubApi: HubAppApi,
        view: ModCategoryComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Category');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategoryName = new TextComponent(view.modCategoryName);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        const modCategory = await this.getModCategory(this.modCategoryID);
        this.modCategoryName.setText(modCategory.Name.DisplayText);
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