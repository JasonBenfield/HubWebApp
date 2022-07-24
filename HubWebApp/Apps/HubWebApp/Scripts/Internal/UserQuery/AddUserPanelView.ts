import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { AddUserFormView } from "../../Lib/Api/AddUserFormView";
import { HubTheme } from "../HubTheme";

export class AddUserPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly addUserForm: AddUserFormView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.addUserForm = mainContent.addView(AddUserFormView);
        this.addUserForm.addOffscreenSubmit();
        this.addUserForm.addContent();
        this.alert = mainContent.addView(MessageAlertView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.cancelButton = HubTheme.instance.commandToolbar.cancelButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.saveButton = HubTheme.instance.commandToolbar.saveButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }
}