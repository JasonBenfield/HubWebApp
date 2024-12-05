import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { Modifier } from "../../../Lib/Modifier";
import { ModifierListCardView } from "./ModifierListCardView";
import { ModifierListItem } from "./ModifierListItem";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListCard {
    private modCategoryID: number;
    private readonly alert: IMessageAlert;
    private readonly modifiers: ListGroup<ModifierListItem, ModifierListItemView>;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ModifierListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Modifiers');
        this.alert = new CardAlert(view.alert);
        this.modifiers = new ListGroup(view.modifiers);
    }

    setModCategoryID(modCategoryID: number) {
        this.modCategoryID = modCategoryID;
    }

    async refresh() {
        const sourceModifiers = await this.getModifiers();
        const modifiers = sourceModifiers.map(m => new Modifier(m));
        this.modifiers.setItems(
            modifiers,
            (sourceItem, listItem) =>
                new ModifierListItem(sourceItem, listItem)
        );
        if (modifiers.length === 0) {
            this.alert.danger('No Modifiers were Found');
        }
    }

    private getModifiers() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ModCategory.GetModifiers(this.modCategoryID)
        );
    }
}