import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModifierCategory } from "../../../Lib/ModifierCategory";
import { ModCategoryComponentView } from "./ModCategoryComponentView";

export class ModCategoryComponent {
    private modCategoryID: number;
    private readonly alert: IMessageAlert;
    private readonly modCategoryName: TextComponent;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ModCategoryComponentView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Category');
        this.alert = new CardAlert(view.alert);
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