import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";
import { UnorderedListView } from "@jasonbenfield/sharedwebapp/Views/UnorderedListView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategoryName: TextBlockView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        let listItem = this.addCardBody()
            .addView(UnorderedListView)
            .addListItem();
        this.modCategoryName = listItem.addView(TextBlockView);
    }
}