import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
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
import { TextSmallView } from "@jasonbenfield/sharedwebapp/Html/TextSmallView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
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
    readonly alert: CardAlertView;
    readonly selectModifierButton: ButtonCommandItem;
    readonly allowAccessButton: ButtonCommandItem;
    readonly denyAccessButton: ButtonCommandItem;
    readonly userRoles: ListGroupView;
    private readonly defaultUserRolesCard: CardView;
    readonly defaultUserRolesTitle: CardTitleHeaderView;
    readonly defaultUserRoles: ListGroupView;

    constructor() {
        super();
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());

        let userHeading = flexFill.addContent(new Heading1());
        this.personName = userHeading.addContent(new TextSpanView());
        this.userName = userHeading.addContent(new TextSmallView());
        userHeading.setMargin(MarginCss.bottom(3));

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
        let navPills = body.addContent(new NavView());
        navPills.pills();
        navPills.setFlexCss(new FlexCss().column().fill());
        this.selectModifierButton = this.addNavLinkButton(navPills);
        this.selectModifierButton.icon.setName('pencil-alt');
        this.selectModifierButton.icon.solidStyle();
        this.selectModifierButton.setText('Select Different Modifier');

        this.allowAccessButton = this.addNavLinkButton(navPills);
        this.allowAccessButton.icon.setName('check');
        this.allowAccessButton.icon.solidStyle();
        this.allowAccessButton.setText('Allow Access');

        this.denyAccessButton = this.addNavLinkButton(navPills);
        this.denyAccessButton.icon.setName('ban');
        this.denyAccessButton.icon.solidStyle();
        this.denyAccessButton.setText('Deny Access');

        this.alert = card.addCardAlert();
        this.userRoles = card.addBlockListGroup(() => new UserRoleListItemView());
        card.setMargin(MarginCss.bottom(3));

        this.defaultUserRolesCard = flexFill.addContent(new CardView());
        this.defaultUserRolesTitle = this.defaultUserRolesCard.addCardTitleHeader();
        this.defaultUserRoles = this.defaultUserRolesCard.addUnorderedListGroup(
            () => new UserRoleListItemView()
        );
    }

    private addNavLinkButton(navPills: NavView) {
        let navLinkButton = navPills.addContent(new ButtonCommandItem());
        navLinkButton.addCssName('nav-link');
        navLinkButton.setTextCss(new TextCss().start());
        return navLinkButton;
    }

    showDefaultUserRoles() {
        this.defaultUserRolesCard.show();
    }

    hideDefaultUserRoles() {
        this.defaultUserRolesCard.hide();
    }
}