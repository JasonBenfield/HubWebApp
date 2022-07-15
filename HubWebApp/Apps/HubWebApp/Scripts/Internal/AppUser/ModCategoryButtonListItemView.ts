import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";

export class ModCategoryButtonListItemView extends ButtonListGroupItemView {
    readonly categoryName: TextBlockView;

    constructor(container: BasicComponentView) {
        super(container);
        this.categoryName = this.addView(TextBlockView);
    }
}