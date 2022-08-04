import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupInputView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
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
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const heading = mainContent.addView(TextHeading3View);
        heading.setText('Add User Group');
        this.form = mainContent.addView(FormView);
        const formGroupGrid = this.form.addView(FormGroupGridView);
        const groupNameFormGroup = formGroupGrid.addFormGroup(FormGroupInputView);
        groupNameFormGroup.caption.setText('Group Name');
        this.groupNameInput = groupNameFormGroup.input;
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

    handleFormSubmitted(action: (el: HTMLElement, evt: JQueryEventObject) => void) {
        this.form
            .onSubmit()
            .execute(action)
            .subscribe();
    }
}