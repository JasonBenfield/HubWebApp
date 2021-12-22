import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ResourceComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly resourceName: TextSpan;
    private readonly resultType: TextSpan;
    private readonly anonListItem: ListItem;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let listGroup = this.addUnorderedListGroup();
        let listItem = new ListItem();
        listGroup.addItem(listItem);
        let row = listItem.addContent(new Row());
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan());
        this.resultType = row.addColumn()
            .addContent(new TextSpan());
        this.anonListItem = new ListItem();
        listGroup.addItem(this.anonListItem);
        this.anonListItem.addContent(new Row())
            .addColumn()
            .addContent(new TextSpan('Anonymous is Allowed'));
        this.anonListItem.hide();
    }

    setResourceName(resourceName: string) { this.resourceName.setText(resourceName); }

    setResultType(resultType: string) { this.resultType.setText(resultType); }

    showAnon() {
        this.anonListItem.show();
    }

    hideAnon() {
        this.anonListItem.hide();
    }
}