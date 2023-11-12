import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { ModifierCategoryListCardView } from "./ModifierCategoryListCardView";
import { ModifierCategoryListItem } from "./ModifierCategoryListItem";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

type Events = { modCategorySelected: IModifierCategoryModel };

export class ModifierCategoryListCard {
    private readonly alert: MessageAlert;
    private readonly modCategories: ListGroup<ModifierCategoryListItem, ModifierCategoryListItemView>;
    private readonly eventSource = new EventSource<Events>(this, { modCategorySelected: null });
    readonly when = this.eventSource.when;

    constructor(
        private readonly hubClient: HubAppClient,
        view: ModifierCategoryListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Categories');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategories = new ListGroup(view.modCategories);
        this.modCategories.when.itemClicked.then(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ModifierCategoryListItem) {
        this.eventSource.events.modCategorySelected.invoke(item.modCategory);
    }

    async refresh() {
        const modCategories = await this.getModCategories();
        this.modCategories.setItems(
            modCategories,
            (modCategory, itemView) =>
                new ModifierCategoryListItem(modCategory, itemView)
        );
        if (modCategories.length === 0) {
            this.alert.danger('No Modifier Categories were Found');
        }
    }

    private getModCategories() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.App.GetModifierCategories()
        );
    }

}