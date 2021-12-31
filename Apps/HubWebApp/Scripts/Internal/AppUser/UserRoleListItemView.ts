import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { HubTheme } from "../HubTheme";

export class UserRoleListItemView extends ListGroupItemView {
    readonly roleName: ITextComponentView;
    readonly deleteButton: ButtonCommandItem;

    constructor() {
        super();
        let row = this.addContent(new Row());
        let col1 = row.addColumn();
        let roleName = col1.addContent(new TextBlockView());
        roleName.setPadding(PaddingCss.top(1));
        this.roleName = roleName;
        let col2 = row.addColumn();
        col2.setColumnCss(ColumnCss.xs('auto'));
        this.deleteButton = col2.addContent(
            HubTheme.instance.listItem.deleteButton()
        );
    }
}