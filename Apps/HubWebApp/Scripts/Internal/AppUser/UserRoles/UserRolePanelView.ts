import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { HubTheme } from "../../HubTheme";
import { EditUserRoleListItemView } from "./EditUserRoleListItemView";

export class UserRolePanelView extends Block {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    readonly userRoles: ListGroupView;
    readonly backButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        let card = flexFill.addContent(new CardView());
        this.titleHeader = card.addCardTitleHeader();
        this.alert = card.addCardAlert().alert;
        this.userRoles = card.addBlockListGroup(
            () => new EditUserRoleListItemView()
        );
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
    }
}