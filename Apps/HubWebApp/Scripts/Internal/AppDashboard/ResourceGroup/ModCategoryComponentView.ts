import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly listGroup: ListGroupView;
    private readonly modCategoryName: TextSpan;
    readonly clicked: IEventHandler<any>;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        this.listGroup = this.addBlockListGroup();
        let listItem = new ListItem();
        this.listGroup.addItem(listItem)
        this.modCategoryName = listItem.addContent(new TextSpan());
        this.clicked = this.listGroup.itemClicked;
    }

    showModCategory() { this.listGroup.show(); }

    hideModCategory() { this.listGroup.hide(); }

    setModCategoryName(name: string) { this.modCategoryName.setText(name); }
}