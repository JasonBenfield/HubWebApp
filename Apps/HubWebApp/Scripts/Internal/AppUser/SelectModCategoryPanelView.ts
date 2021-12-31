﻿import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { HubTheme } from "../HubTheme";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";

export class SelectModCategoryPanelView extends Block {
    private readonly defaultModListItem: ButtonListGroupItemView;
    readonly defaultModClicked: IEventHandler<any>;
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    readonly modCategories: ListGroupView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        this.setName(SelectModCategoryPanelView.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.defaultModListItem = new ButtonListGroupItemView();
        this.defaultModClicked = this.defaultModListItem.clicked;
        this.defaultModListItem
            .addContent(new TextBlockView())
            .configure(tb => tb.setText('Default Modifier'));
        let defaultModList = flexFill.addContent(new ListGroupView(() => new ListItem()));
        defaultModList.addItem(this.defaultModListItem);
        defaultModList.setMargin(MarginCss.bottom(3));

        let card = flexFill.addContent(new CardView());
        this.titleHeader = card.addCardTitleHeader();
        this.alert = card.addCardAlert().alert;
        this.modCategories = card.addBlockListGroup(
            () => new ModCategoryButtonListItemView()
        );
        card.setMargin(MarginCss.bottom(3));

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
    }
}