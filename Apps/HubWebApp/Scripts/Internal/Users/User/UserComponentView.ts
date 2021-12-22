import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FormGroupView } from "@jasonbenfield/sharedwebapp/Html/FormGroupView";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { HubTheme } from "../../HubTheme";

export class UserComponentView extends CardView {
    readonly alert: MessageAlertView;
    private readonly userName: TextSpan;
    private readonly fullName: TextSpan;
    private readonly email: TextSpan;
    readonly editButton: ButtonCommandItem;

    constructor() {
        super();
        this.setName(UserComponentView.name);
        let headerRow = this.addCardHeader()
            .addContent(new Row());
        headerRow.addColumn()
            .addContent(new TextSpan('User'));
        this.editButton = headerRow.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(HubTheme.instance.cardHeader.editButton());
        this.alert = this.addCardAlert().alert;
        let body = this.addCardBody();
        this.userName = this.addBodyRow(body, 'User Name');
        this.userName.syncTitleWithText();
        this.fullName = this.addBodyRow(body, 'Name');
        this.fullName.syncTitleWithText();
        this.email = this.addBodyRow(body, 'Email');
        this.email.syncTitleWithText();
    }

    setUserName(userName: string) { this.userName.setText(userName); }

    setFullName(fullName: string) { this.fullName.setText(fullName); }

    setEmail(email: string) { this.email.setText(email); }

    private addBodyRow(body: Block, caption: string) {
        let formGroup = body.addContent(new FormGroupView());
        formGroup.captionColumn.addContent(new TextSpan(caption));
        formGroup.captionColumn.setColumnCss(ColumnCss.xs(4));
        formGroup.valueColumn.setTextCss(new TextCss().truncate());
        return formGroup.valueColumn.addContent(new TextSpan())
            .configure(ts => ts.addCssName('form-control-plaintext'));
    }
}