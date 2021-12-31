import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { Heading1 } from "@jasonbenfield/sharedwebapp/Html/Heading1";
import { Heading3 } from "@jasonbenfield/sharedwebapp/Html/Heading3";
import { NavView } from "@jasonbenfield/sharedwebapp/Html/NavView";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Html/TextBlockView";
import { TextSmallView } from "@jasonbenfield/sharedwebapp/Html/TextSmallView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { HubTheme } from "../HubTheme";
import { UserRoleListItemView } from "./UserRoleListItemView";

export class UserRolesPanelView extends Block {
    readonly userName: ITextComponentView;
    readonly personName: ITextComponentView;
    readonly appName: ITextComponentView;
    readonly appType: ITextComponentView;
    readonly categoryName: ITextComponentView;
    readonly modifierDisplayText: ITextComponentView;
    readonly addButton: ButtonCommandItem;
    readonly alert: MessageAlertView;
    readonly selectModifierButton: ButtonCommandItem;
    readonly userRoles: ListGroupView;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());

        let userHeading = flexFill.addContent(new Heading1());
        this.personName = userHeading.addContent(new TextSpanView());
        this.userName = userHeading.addContent(new TextSmallView());

        let appHeading = flexFill.addContent(new Heading3());
        this.appName = appHeading.addContent(new TextSpanView());
        this.appType = appHeading.addContent(new TextSmallView());
        appHeading.setMargin(MarginCss.bottom(3));

        let card = flexFill.addContent(new CardView());
        let categoryHeader = card.addCardTitleHeader();
        let categoryRow = categoryHeader.addContent(new Row());
        let modCol = categoryRow.addColumn()
            .configure(c => c.setPadding(PaddingCss.top(1)));
        this.categoryName = modCol.addContent(new TextSpanView());
        this.modifierDisplayText = modCol.addContent(new TextSpanView());
        modCol.addContent(new TextSpanView())
            .configure(ts => ts.setText('Roles'));
        this.addButton = categoryRow
            .addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(HubTheme.instance.cardHeader.addButton());
        this.addButton.setTitle('Add Role');
        let body = card.addCardBody();
        let bodyContainer = body.addContent(new NavView());
        bodyContainer.pills();
        bodyContainer.setFlexCss(new FlexCss().column().fill());
        this.selectModifierButton = bodyContainer.addContent(new ButtonCommandItem());
        this.selectModifierButton.addCssName('nav-link');
        this.selectModifierButton.icon.setName('hand-pointer');
        this.selectModifierButton.icon.regularStyle();
        this.selectModifierButton.setText('Select modifier');
        this.selectModifierButton.setTextCss(new TextCss().start());
        this.alert = card.addCardAlert().alert;
        this.userRoles = card.addBlockListGroup(() => new UserRoleListItemView());
    }
}