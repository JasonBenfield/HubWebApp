import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ResourceGroupComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly groupName: TextSpanView;
    private readonly anonListItem: ListItem;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        let listGroup = this.addUnorderedListGroup();
        let listItem = new ListItem();
        listGroup.addItem(listItem);
        let row = listItem.addContent(new Row());
        this.groupName = row.addColumn()
            .addContent(new TextSpanView());
        this.anonListItem = new ListItem();
        listGroup.addItem(this.anonListItem);
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpanView())
            .configure(ts => ts.setText('Anonymous is Allowed'));
    }

    showAnonMessage() {
        this.anonListItem.show();
    }

    hideAnonMessage() {
        this.anonListItem.hide();
    }
}