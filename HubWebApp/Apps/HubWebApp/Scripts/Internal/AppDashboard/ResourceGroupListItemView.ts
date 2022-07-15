import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";

export class ResourceGroupListItemView extends ButtonListGroupItemView {
    readonly groupName: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        this.groupName = row.addColumn()
            .addView(TextSpanView);
    }
}