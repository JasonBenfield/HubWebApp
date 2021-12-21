import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { UnorderedList } from "@jasonbenfield/sharedwebapp/Html/UnorderedList";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly modCategoryName: TextBlock;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        let listItem = new ListItem();
        this.addCardBody()
            .addContent(new UnorderedList())
            .addItem(listItem);
        this.modCategoryName = listItem.addContent(new TextBlock());
    }

    setModCategoryName(categoryName: string) { this.modCategoryName.setText(categoryName); }
}