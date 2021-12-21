import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { FaIcon } from "@jasonbenfield/sharedwebapp/FaIcon";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class RoleAccessListItemView extends ListGroupItemView {
    private icon: FaIcon;
    private roleName: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.icon = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new FaIcon());
        this.icon.regularStyle();
        this.icon.makeFixedWidth();
        this.roleName = row.addColumn()
            .addContent(new TextSpan());
    }

    allowAccess() {
        this.updateIsAllowed(true);
    }

    denyAccess() {
        this.updateIsAllowed(false);
    }

    private updateIsAllowed(isAllowed: boolean) {
        this.icon.setName(isAllowed ? 'thumbs-up' : 'thumbs-down');
        this.icon.addCssFrom(
            new TextCss().context(
                isAllowed
                    ? ContextualClass.success
                    : ContextualClass.danger
            ).cssClass()
        );
    }

    setRoleName(roleName: string) { this.roleName.setText(roleName); }
}