import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";

export class ResourceListItemView extends ButtonListGroupItemView {
    readonly resourceName: TextSpanView;
    readonly resultType: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(8)))
            .addView(TextSpanView);
        this.resultType = row.addColumn()
            .addView(TextSpanView);
    }
}