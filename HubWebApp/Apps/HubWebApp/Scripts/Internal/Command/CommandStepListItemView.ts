import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { GridListGroupItemView, GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";

export class CommandStepListItemView extends GridListGroupItemView {
    static setTemplateColumns(listView: GridListGroupView<CommandStepListItemView>) {
        listView.setTemplateColumns(
            CssLengthUnit.auto(),
            CssLengthUnit.auto(),
            CssLengthUnit.auto(),
            CssLengthUnit.flex(1)
        );
    }

    readonly activityTextView: BasicTextComponentView;
    readonly timeStartedTextView: BasicTextComponentView;
    readonly timeEndedTextView: BasicTextComponentView;
    readonly errorMessageTextView: BasicTextComponentView;

    constructor(container: BasicComponentView) {
        super(container);
        const cell1 = this.addCell();
        this.activityTextView = cell1.addView(TextBlockView);
        const cell2 = this.addCell();
        this.timeStartedTextView = cell2.addView(TextBlockView);
        const cell3 = this.addCell();
        this.timeEndedTextView = cell3.addView(TextBlockView);
        const cell4 = this.addCell();
        this.errorMessageTextView = cell4.addView(TextBlockView);
    }

    styleAsFailed() {
        this.setContext(ContextualClass.danger);
    }

    styleAsInProgress() {
        this.setContext(ContextualClass.info);
    }
}