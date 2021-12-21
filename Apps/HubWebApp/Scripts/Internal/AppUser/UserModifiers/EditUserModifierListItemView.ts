import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { FaIcon } from "@jasonbenfield/sharedwebapp/FaIcon";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class EditUserModifierListItemView extends ButtonListGroupItemView {
    private readonly modKey: TextSpan;
    private readonly modDisplayText: TextSpan;
    private readonly icon: FaIcon;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.icon = row.addColumn()
            .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new FaIcon('square'))
            .configure(icon => {
                icon.makeFixedWidth();
                icon.regularStyle();
            });
        this.modKey = row.addColumn()
            .addContent(new TextSpan(''));
    }

    setModKey(modKey: string) { this.modKey.setText(modKey); }

    setModDisplayText(displayText: string) { this.modDisplayText.setText(displayText); }

    startAssignment() {
        this.disable();
        this.icon.solidStyle();
        this.icon.setName('sync-alt');
        this.icon.startAnimation('spin');
    }

    assign() {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass.success);
    }

    unassign() {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass.default);
    }

    endAssignment() {
        this.enable();
        this.icon.stopAnimation();
    }
}