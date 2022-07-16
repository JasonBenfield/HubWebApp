import { LinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class AppListItemView extends LinkListGroupItemView {
    readonly appName: TextSpanView;
    readonly appTitle: TextSpanView;
    readonly appType: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.appName = row.addColumn()
            .addView(TextSpanView);
        this.appTitle = row.addColumn()
            .addView(TextSpanView);
        this.appType = row.addColumn()
            .addView(TextSpanView);
    }
}