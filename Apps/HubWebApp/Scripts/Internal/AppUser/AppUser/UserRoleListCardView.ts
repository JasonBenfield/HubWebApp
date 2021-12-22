import { AlignCss } from "@jasonbenfield/sharedwebapp/AlignCss";
import { CardHeader } from "@jasonbenfield/sharedwebapp/Card/CardHeader";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { HubTheme } from "../../HubTheme";
import { RoleListItemView } from "./RoleListItemView";

export class UserRoleListCardView extends CardView {
    readonly alert: MessageAlertView;
    readonly roles: ListGroupView;
    readonly editButton: ButtonCommandItem;

    constructor() {
        super();
        let row = this.addContent(new CardHeader())
            .addContent(new Row())
            .configure(r => r.addCssFrom(new AlignCss().items(a => a.xs('baseline')).cssClass()));
        row.addColumn()
            .addContent(new TextSpan('User Roles'));
        this.editButton = row.addColumn()
            .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
            .addContent(HubTheme.instance.cardHeader.editButton());
        this.alert = this.addCardAlert().alert;
        this.roles = this.addUnorderedListGroup(() => new RoleListItemView());
    }
}