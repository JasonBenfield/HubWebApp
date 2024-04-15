import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModCategoryComponentView } from "./ModCategoryComponentView";
import { ModifierCategory } from "../../../Lib/ModifierCategory";

export class ModCategoryComponent {
    private modCategoryID: number;
    private readonly alert: MessageAlert;
    private readonly modCategoryName: TextComponent;

    constructor(
        private readonly hubClient: HubAppClient,
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
        const sourceModCategory = await this.getModCategory(this.modCategoryID);
        const modCategory = new ModifierCategory(sourceModCategory);
        this.modCategoryName.setText(modCategory.name.displayText);
    }

    private getModCategory(modCategoryID: number) {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ModCategory.GetModCategory(modCategoryID)
        );
    }
}