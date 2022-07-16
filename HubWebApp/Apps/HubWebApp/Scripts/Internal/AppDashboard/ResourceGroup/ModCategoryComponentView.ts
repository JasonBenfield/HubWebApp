import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly listGroup: ListGroupView;
    readonly modCategoryName: TextSpanView;
    readonly clicked: IEventHandler<any>;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.listGroup = this.addView(ListGroupView);
        const listItem = this.listGroup.addListGroupItem();
        this.modCategoryName = listItem.addView(TextSpanView);
    }

    showModCategory() { this.listGroup.show(); }

    hideModCategory() { this.listGroup.hide(); }
}