import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { TextValueFormGroupView } from "@jasonbenfield/sharedwebapp/Html/TextValueFormGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { HubTheme } from "../../HubTheme";

export class UserComponentView extends CardView {
    readonly alert: MessageAlertView;
    readonly userName: TextValueFormGroupView;
    readonly fullName: TextValueFormGroupView;
    readonly email: TextValueFormGroupView;
    readonly editButton: ButtonCommandItem;

    constructor() {
        super();
        this.setName(UserComponentView.name);
        let headerRow = this.addCardHeader()
            .addContent(new Row());
        headerRow.addColumn()
            .addContent(new TextSpanView())
            .configure(ts => ts.setText('User'));
        this.editButton = headerRow.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(HubTheme.instance.cardHeader.editButton());
        this.alert = this.addCardAlert().alert;
        let body = this.addCardBody();
        this.userName = this.addBodyRow(body);
        this.fullName = this.addBodyRow(body);
        this.email = this.addBodyRow(body);
    }

    private addBodyRow(body: Block) {
        let formGroup = body.addContent(new TextValueFormGroupView());
        formGroup.captionColumn.setColumnCss(ColumnCss.xs(4));
        formGroup.valueColumn.setTextCss(new TextCss().truncate());
        return formGroup;
    }
}