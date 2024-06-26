﻿import { CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { EditUserFormView } from "../../Lib/Http/EditUserFormView";
import { HubTheme } from "../HubTheme";

export class UserEditPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly editUserForm: EditUserFormView;
    readonly titleHeader: CardTitleHeaderView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.setViewName(UserEditPanelView.name);
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.cancelButton = toolbar.columnEnd.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.cancelButton(this.cancelButton);
        this.saveButton = toolbar.columnEnd.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.saveButton(this.saveButton);
        const editCard = mainContent.addView(CardView);
        this.titleHeader = editCard.addCardTitleHeader();
        let cardBody = editCard.addCardBody();
        this.editUserForm = cardBody.addView(EditUserFormView);
        this.editUserForm.addContent();
        this.editUserForm.addOffscreenSubmit();
    }
}