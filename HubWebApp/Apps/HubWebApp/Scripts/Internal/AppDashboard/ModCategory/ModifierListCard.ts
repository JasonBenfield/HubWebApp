import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModifierListCardView } from "./ModifierListCardView";
import { ModifierListItem } from "./ModifierListItem";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListCard {
    private modCategoryID: number;
    private readonly alert: MessageAlert;
    private readonly modifiers: ListGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModifierListCardView
    ) {
        new TextBlock('Modifiers', this.view.titleHeader);
        this.alert = new CardAlert(this.view.alert).alert;
        this.modifiers = new ListGroup(this.view.modifiers);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        let modifiers = await this.getModifiers();
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