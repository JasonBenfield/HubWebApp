import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ResourceComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly resourceName: TextSpanView;
    readonly resultType: TextSpanView;
    private readonly anonListItem: ListItem;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        let listGroup = this.addUnorderedListGroup();
        let listItem = new ListItem();
        listGroup.addItem(listItem);
        let row = listItem.addContent(new Row());
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpanView());
        this.resultType = row.addColumn()
            .addContent(new TextSpanView());
        this.anonListItem = new ListItem();
        listGroup.addItem(this.anonListItem);
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpanView())
            .configure(ts => ts.setText('Anonymous is Allowed'));
        this.anonListItem.hide();
    }

    showAnon() {
        this.anonListItem.show();
    }

    hideAnon() {
        this.anonListItem.hide();
    }
}