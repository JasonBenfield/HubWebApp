import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";

export class RequestExpandedListItemView extends ListGroupItemView {
    readonly timeStarted: TextSpanView;
    readonly groupName: TextSpanView;
    readonly actionName: TextSpanView;
    readonly userName: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.timeStarted = row.addColumn()
            .addView(TextSpanView);
        this.groupName = row.addColumn()
            .addView(TextSpanView);
        this.actionName = row.addColumn()
            .addView(TextSpanView);
        this.userName = row.addColumn()
            .addView(TextSpanView);
    }
}