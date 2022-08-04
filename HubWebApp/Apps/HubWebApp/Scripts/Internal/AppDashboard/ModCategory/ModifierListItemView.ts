import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";

export class ModifierListItemView extends ListGroupItemView {
    readonly modKey: TextSpanView;
    readonly displayText: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.modKey = row.addColumn()
            .configure(c => {
                c.setColumnCss(ColumnCss.xs(4));
                c.addCssFrom(new TextCss().truncate().cssClass());
            })
            .addView(TextSpanView);
        this.displayText = row.addColumn()
            .configure(c => c.addCssFrom(new TextCss().truncate().cssClass()))
            .addView(TextSpanView);
    }
}