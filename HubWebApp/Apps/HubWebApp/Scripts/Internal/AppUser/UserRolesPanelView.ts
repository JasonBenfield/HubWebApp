﻿import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { Heading1View, Heading3View } from "@jasonbenfield/sharedwebapp/Views/Headings";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSmallView } from "@jasonbenfield/sharedwebapp/Views/TextSmallView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { HubTheme } from "../HubTheme";
import { UserRoleListItem } from "./UserRoleListItem";
import { UserRoleListItemView } from "./UserRoleListItemView";

export class UserRolesPanelView extends GridView {
    readonly userName: BasicTextComponentView;
    readonly personName: BasicTextComponentView;
    readonly appName: BasicTextComponentView;
    readonly appType: BasicTextComponentView;
    readonly categoryName: BasicTextComponentView;
    readonly modifierDisplayText: BasicTextComponentView;
    readonly addButton: ButtonCommandView;
    readonly alert: CardAlertView;
    readonly selectModifierButton: ButtonCommandView;
    readonly allowAccessButton: ButtonCommandView;
    readonly denyAccessButton: ButtonCommandView;
    readonly userRoles: ListGroupView;
    private readonly defaultUserRolesCard: CardView;
    readonly defaultUserRolesTitle: CardTitleHeaderView;
    readonly defaultUserRoles: ListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const userHeading = mainContent.addView(Heading1View);
        this.personName = userHeading.addView(TextSpanView);
        this.userName = userHeading.addView(TextSmallView);
        userHeading.setMargin(MarginCss.bottom(3));

        const appHeading = mainContent.addView(Heading3View);
        this.appName = appHeading.addView(TextSpanView);
        this.appType = appHeading.addView(TextSmallView);
        appHeading.setMargin(MarginCss.bottom(3));

        const card = mainContent.addView(CardView);
        const categoryHeader = card.addCardHeader();
        const categoryRow = categoryHeader.addView(RowView);
        const modCol = categoryRow.addColumn()
            .configure(c => c.setPadding(PaddingCss.top(1)));
        this.categoryName = modCol.addView(TextSpanView);
        this.modifierDisplayText = modCol.addView(TextSpanView);
        modCol.addView(TextSpanView)
            .configure(ts => ts.setText('Roles'));
        this.addButton = categoryRow
            .addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(ButtonCommandView)
            .configure(b => HubTheme.instance.cardHeader.addButton(b));
        this.addButton.setTitle('Add Role');
        const body = card.addCardBody();
        const navPills = body.addView(NavView);
        navPills.pills();
        navPills.setFlexCss(new FlexCss().column().fill());
        this.selectModifierButton = this.addNavLinkButton(navPills);
        this.selectModifierButton.icon.solidStyle('pencil-alt');
        this.selectModifierButton.setText('Select Different Modifier');

        this.allowAccessButton = this.addNavLinkButton(navPills);
        this.allowAccessButton.icon.solidStyle('check');
        this.allowAccessButton.setText('Allow Access');

        this.denyAccessButton = this.addNavLinkButton(navPills);
        this.denyAccessButton.icon.solidStyle('ban');
        this.denyAccessButton.setText('Deny Access');

        this.alert = card.addCardAlert();
        this.userRoles = card.addView(ListGroupView);
        this.userRoles.setItemViewType(UserRoleListItemView);
        card.setMargin(MarginCss.bottom(3));

        this.defaultUserRolesCard = mainContent.addView(CardView);
        this.defaultUserRolesTitle = this.defaultUserRolesCard.addCardTitleHeader();
        this.defaultUserRoles = this.defaultUserRolesCard.addUnorderedListGroup();
        this.defaultUserRoles.setItemViewType(UserRoleListItemView);
    }

    handleUserRoleDeleteClicked(action: (el: HTMLElement) => void) {
        this.userRoles.on('click')
            .select('button.deleteButton')
            .execute(action)
            .subscribe();
    }

    private addNavLinkButton(navPills: NavView) {
        const navLinkButton = navPills.addView(ButtonCommandView);
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