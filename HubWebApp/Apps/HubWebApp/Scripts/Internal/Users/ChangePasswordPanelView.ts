import { MessageAlert } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/MessageAlert";
import { CssLengthUnit } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { ButtonCommandView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Command";
import { GridView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
import { MessageAlertView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/MessageAlertView";
import { ToolbarView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { ChangePasswordFormView } from '../../Lib/Api/ChangePasswordFormView';
import { MarginCss } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/MarginCss";

export class ChangePasswordPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly changePasswordForm: ChangePasswordFormView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(ChangePasswordPanelView.name);
        this.height100();
        this.layout();
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