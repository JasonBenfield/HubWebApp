import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { ModifierCategoryListCardView } from "./ModifierCategoryListCardView";
import { ModifierCategoryListItem } from "./ModifierCategoryListItem";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListCard {
    private readonly alert: MessageAlert;
    private readonly modCategories: ListGroup<ModifierCategoryListItem, ModifierCategoryListItemView>;

    private readonly _modCategorySelected = new DefaultEvent<IModifierCategoryModel>(this);
    readonly modCategorySelected = this._modCategorySelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        view: ModifierCategoryListCardView
    ) {
        new TextComponent(view.titleHeader).setText('Modifier Categories');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategories = new ListGroup(view.modCategories);
        this.modCategories.registerItemClicked(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ModifierCategoryListItem) {
        this._modCategorySelected.invoke(item.modCategory);
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

    private async getModCategories() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.App.GetModifierCategories()
        );
    }

}