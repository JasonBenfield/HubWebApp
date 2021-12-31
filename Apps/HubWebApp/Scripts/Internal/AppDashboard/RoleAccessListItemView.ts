import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { FaIcon } from "@jasonbenfield/sharedwebapp/FaIcon";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";

export class RoleAccessListItemView extends ListGroupItemView {
    private readonly icon: FaIcon;
    readonly roleName: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.icon = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new FaIcon());
        this.icon.regularStyle();
        this.icon.makeFixedWidth();
        this.roleName = row.addColumn()
            .addContent(new TextSpanView());
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
}