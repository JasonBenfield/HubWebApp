import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { ModifierCategoryListCardView } from "./ModifierCategoryListCardView";
import { ModifierCategoryListItem } from "./ModifierCategoryListItem";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListCard {
    private readonly alert: MessageAlert;
    private readonly modCategories: ListGroup;

    private readonly _modCategorySelected = new DefaultEvent<IModifierCategoryModel>(this);
    readonly modCategorySelected = this._modCategorySelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: ModifierCategoryListCardView
    ) {
        new TextBlock('Modifier Categories', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.modCategories = new ListGroup(this.view.modCategories);
        this.modCategories.itemClicked.register(this.onItemSelected.bind(this));
    }

    private onItemSelected(item: ModifierCategoryListItem) {
        this._modCategorySelected.invoke(item.modCategory);
    }

    async refresh() {
        let modCategories = await this.getModCategories();
        this.modCategories.setItems(
            modCategories,
            (modCategory, itemView: ModifierCategoryListItemView) =>
                new ModifierCategoryListItem(modCategory, itemView)
        );
        if (modCategories.length === 0) {
            this.alert.danger('No Modifier Categories were Found');
        }
    }

    private async getModCategories() {
        let modCategories: IModifierCategoryModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategories = await this.hubApi.App.GetModifierCategories();
            }
        );
        return modCategories;
    }

}