import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { ChangePasswordFormView } from '../../Lib/Http/ChangePasswordFormView';
import { HubTheme } from "../HubTheme";

export class ChangePasswordPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly changePasswordForm: ChangePasswordFormView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(ChangePasswordPanelView.name);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        this.changePasswordForm = mainContent.addView(ChangePasswordFormView);
        this.changePasswordForm.addContent();
        this.changePasswordForm.addOffscreenSubmit();
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.cancelButton = HubTheme.instance.commandToolbar.cancelButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
        this.saveButton = HubTheme.instance.commandToolbar.saveButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }
}