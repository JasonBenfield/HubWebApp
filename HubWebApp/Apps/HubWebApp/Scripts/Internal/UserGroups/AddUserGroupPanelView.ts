import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupInputView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { FormView } from "@jasonbenfield/sharedwebapp/Views/FormView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { InputView } from "@jasonbenfield/sharedwebapp/Views/InputView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";

export class AddUserGroupPanelView extends GridView {
    readonly alert: MessageAlertView;
    private readonly form: FormView;
    readonly groupNameInput: InputView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const heading = mainContent.addView(TextHeading3View);
        heading.setText('Add User Group');
        this.form = mainContent.addView(FormView);
        const formGroupGrid = this.form.addView(FormGroupContainerView);
        const groupNameFormGroup = formGroupGrid.addFormGroup(FormGroupInputView);
        groupNameFormGroup.caption.setText('Group Name');
        this.groupNameInput = groupNameFormGroup.inputView;
        this.form.addOffscreenSubmit();
        this.alert = mainContent.addView(MessageAlertView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addView(ToolbarView)
        );
        this.cancelButton = HubTheme.instance.commandToolbar.cancelButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
        this.saveButton = HubTheme.instance.commandToolbar.saveButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }

    handleFormSubmitted(action: (el: HTMLElement, evt: JQuery.Event) => void) {
        this.form
            .onSubmit()
            .execute(action)
            .subscribe();
    }
}