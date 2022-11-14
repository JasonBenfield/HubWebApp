import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { TextHeading1View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";

export class UserPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly menuButton: ButtonCommandView;
    readonly userName: BasicTextComponentView;
    readonly personName: BasicTextComponentView;
    readonly email: BasicTextComponentView;
    readonly editButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        mainContent.addView(TextHeading1View).configure(h => h.setText('Current User'));
        const formGroupGrid = mainContent.addView(FormGroupGridView);
        const userNameFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        userNameFormGroup.caption.setText('User Name');
        this.userName = userNameFormGroup.textValue;
        const personNameFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        personNameFormGroup.caption.setText('Name');
        this.personName = personNameFormGroup.textValue;
        const emailFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        emailFormGroup.caption.setText('Email');
        this.email = emailFormGroup.textValue;
        this.alert = mainContent.addView(MessageAlertView);
        this.editButton = mainContent.addView(ButtonCommandView);
        this.editButton.icon.solidStyle('edit');
        this.editButton.useOutlineStyle(ContextualClass.primary);
        this.editButton.setText('Edit');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addCell().addView(ToolbarView));
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }
}