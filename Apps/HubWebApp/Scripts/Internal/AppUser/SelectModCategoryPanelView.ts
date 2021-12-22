import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { ListBlockViewModel } from "@jasonbenfield/sharedwebapp/Html/ListBlockViewModel";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { UnorderedList } from "@jasonbenfield/sharedwebapp/Html/UnorderedList";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { HubTheme } from "../HubTheme";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class SelectModCategoryPanelView extends Block {
    private readonly defaultModListItem: ButtonListGroupItemView;
    readonly defaultModClicked: IEventHandler<any>;
    readonly modCategories: ListGroupView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        flexFill.setPadding(PaddingCss.top(3));

        this.defaultModListItem = new ButtonListGroupItemView();
        this.defaultModClicked = this.defaultModListItem.clicked;
        this.defaultModListItem.addContent(new TextBlock('Default Modifier'));
        let defaultModList = flexFill.addContent(new UnorderedList());
        defaultModList.addItem(this.defaultModListItem);
        defaultModList.setMargin(MarginCss.bottom(3));

        this.modCategories = this.addContent(
            new ListGroupView(
                () => new ModCategoryButtonListItemView(),
                new ListBlockViewModel()
            )
        );
        this.modCategories.setMargin(MarginCss.bottom(3));

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
    }
}