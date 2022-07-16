import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ModifierListCardView } from "./ModifierListCardView";
import { ModifierListItem } from "./ModifierListItem";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListCard {
    private modCategoryID: number;
    private readonly alert: MessageAlert;
    private readonly modifiers: ListGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        view: ModifierListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Modifiers');
        this.alert = new CardAlert(view.alert).alert;
        this.modifiers = new ListGroup(view.modifiers);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        const modifiers = await this.getModifiers();
        this.modifiers.setItems(
            modifiers,
            (sourceItem: IModifierModel, listItem: ModifierListItemView) =>
                new ModifierListItem(sourceItem, listItem)
        );
        if (modifiers.length === 0) {
            this.alert.danger('No Modifiers were Found');
        }
    }

    private async getModifiers() {
        let modifiers: IModifierModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modifiers = await this.hubApi.ModCategory.GetModifiers(this.modCategoryID);
            }
        );
        return modifiers;
    }
}