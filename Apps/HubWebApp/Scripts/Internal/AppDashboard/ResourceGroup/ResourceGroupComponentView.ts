import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ResourceGroupComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly groupName: TextSpan;
    private readonly anonListItem: ListItem;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let listGroup = this.addUnorderedListGroup();
        let listItem = new ListItem();
        listGroup.addItem(listItem);
        let row = listItem.addContent(new Row());
        this.groupName = row.addColumn()
            .addContent(new TextSpan());
        this.anonListItem = new ListItem();
        listGroup.addItem(this.anonListItem);
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan('Anonymous is Allowed'));
    }

    showAnonMessage() {
        this.anonListItem.show();
    }

    hideAnonMessage() {
        this.anonListItem.hide();
    }

    setGroupName(groupName: string) { this.groupName.setText(groupName); }
}