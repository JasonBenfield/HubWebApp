import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { UnorderedList } from "@jasonbenfield/sharedwebapp/Html/UnorderedList";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategoryName: TextBlockView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        let listItem = this.addCardBody()
            .addContent(new UnorderedList())
            .addListItem();
        this.modCategoryName = listItem.addContent(new TextBlockView());
    }
}