import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class UserListItemView extends ButtonListGroupItemView {
    readonly userName: TextSpanView;
    readonly fullName: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        let row = this.addView(RowView);
        this.userName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs(4)))
            .addView(TextSpanView);
        this.fullName = row.addColumn()
            .addView(TextSpanView);
    }
}