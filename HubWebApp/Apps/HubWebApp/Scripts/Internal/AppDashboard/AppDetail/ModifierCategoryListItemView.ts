import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class ModifierCategoryListItemView extends ButtonListGroupItemView {
    readonly categoryName: TextSpanView;

    constructor(container: BasicComponentView) {
        super(container);
        this.categoryName = this.addView(RowView)
            .addColumn()
            .addView(TextSpanView);
    }
}