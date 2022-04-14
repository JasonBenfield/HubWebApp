import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    private readonly listGroup: ListGroupView;
    readonly modCategoryName: TextSpanView;
    readonly clicked: IEventHandler<any>;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.listGroup = this.addBlockListGroup();
        let listItem = this.listGroup.addListGroupItem();
        this.modCategoryName = listItem.addContent(new TextSpanView());
        this.clicked = this.listGroup.itemClicked;
    }

    showModCategory() { this.listGroup.show(); }

    hideModCategory() { this.listGroup.hide(); }
}