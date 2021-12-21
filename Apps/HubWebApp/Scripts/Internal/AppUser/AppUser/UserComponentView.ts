import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class UserComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly cardBody: Block;
    private readonly userName: TextSpan;
    private readonly fullName: TextSpan;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        this.cardBody = this.addCardBody();
        let row = this.cardBody.addContent(new Row());
        this.userName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan('User'));
        this.fullName = row.addColumn()
            .addContent(new TextSpan());
        this.cardBody.hide();
    }

    setUserName(userName: string) { this.userName.setText(userName); }

    setFullName(fullName: string) { this.fullName.setText(fullName); }

    showCardBody() { this.cardBody.show(); }

    hideCardBody() { this.cardBody.hide(); }
}