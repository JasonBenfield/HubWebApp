import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class ModCategoryButtonListItemView extends ButtonListGroupItemView {
    private readonly categoryName: TextBlock;

    constructor() {
        super();
        this.categoryName = this.addContent(new TextBlock);
    }

    setCategoryName(name: string) { this.categoryName.setText(name); }
}