﻿import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListItem } from "@jasonbenfield/sharedwebapp/Html/ListItem";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ModCategoryComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    private readonly listGroup: ListGroupView;
    readonly modCategoryName: TextSpanView;
    readonly clicked: IEventHandler<any>;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        this.listGroup = this.addBlockListGroup();
        let listItem = new ListItem();
        this.listGroup.addItem(listItem)
        this.modCategoryName = listItem.addContent(new TextSpanView());
        this.clicked = this.listGroup.itemClicked;
    }

    showModCategory() { this.listGroup.show(); }

    hideModCategory() { this.listGroup.hide(); }
}