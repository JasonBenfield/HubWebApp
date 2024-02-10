import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { FlexCss } from "@jasonbenfield/sharedwebapp/FlexCss";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { NavView } from "@jasonbenfield/sharedwebapp/Views/NavView";
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
    readonly changePasswordButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        mainContent.addView(TextHeading1View).configure(h => h.setText('Current User'));
        const formGroupGrid = mainContent.addView(FormGroupContainerView);
        const userNameFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        userNameFormGroup.caption.setText('User Name');
        this.userName = userNameFormGroup.valueTextView;
        const personNameFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        personNameFormGroup.caption.setText('Name');
        this.personName = personNameFormGroup.valueTextView;
        const emailFormGroup = formGroupGrid.addFormGroup(FormGroupTextView);
        emailFormGroup.caption.setText('Email');
        this.email = emailFormGroup.valueTextView;
        this.alert = mainContent.addView(MessageAlertView);
        const nav = mainContent.addView(NavView);
        nav.pills();
        nav.setFlexCss(new FlexCss().column());
        this.editButton = nav.addButtonCommand();
        this.editButton.setTextCss(new TextCss().start());
        this.editButton.icon.solidStyle('edit');
        this.editButton.useOutlineStyle(ContextualClass.primary);
        this.editButton.setText('Edit');
        this.changePasswordButton = nav.addButtonCommand();
        this.changePasswordButton.setTextCss(new TextCss().start());
        this.changePasswordButton.icon.solidStyle('lock');
        this.changePasswordButton.useOutlineStyle(ContextualClass.primary);
        this.changePasswordButton.setText('Change Password');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addCell().addView(ToolbarView));
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }
}