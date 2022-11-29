import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { TextListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modCategoryName: TextListGroupItemView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        const list = this.addUnorderedListGroup(TextListGroupItemView);
        this.modCategoryName = list.addListGroupItem();
    }
}