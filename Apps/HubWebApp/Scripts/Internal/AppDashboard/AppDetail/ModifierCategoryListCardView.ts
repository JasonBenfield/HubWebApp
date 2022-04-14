import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { ModifierCategoryListItemView } from "./ModifierCategoryListItemView";

export class ModifierCategoryListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategories: ListGroupView;

    readonly modCategorySelected: IEventHandler<IListItemView>;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.modCategories = this.addBlockListGroup(() => new ModifierCategoryListItemView());
        this.modCategorySelected = this.modCategories.itemClicked;
    }
}