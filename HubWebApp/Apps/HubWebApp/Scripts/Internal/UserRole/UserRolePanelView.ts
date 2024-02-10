import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonContainerView } from "@jasonbenfield/sharedwebapp/Views/ButtonContainerView";
import { ButtonCommandView, LinkCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { ModalConfirmView, ModalMessageAlertView } from "@jasonbenfield/sharedwebapp/Views/Modal";

export class UserRolePanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly formGroupContainerView: FormGroupContainerView;
    readonly userNameFormGroupView: FormGroupTextView;
    readonly appKeyFormGroupView: FormGroupTextView;
    readonly modCategoryNameFormGroupView: FormGroupTextView;
    readonly modifierNameFormGroupView: FormGroupTextView;
    readonly roleNameFormGroupView: FormGroupTextView;
    readonly appLink: LinkCommandView;
    readonly userLink: LinkCommandView;
    readonly deleteUserRoleLink: ButtonCommandView;
    readonly modalConfirmView: ModalConfirmView;
    readonly modalAlertView: ModalMessageAlertView;
    readonly menuButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        this.formGroupContainerView = mainContent.addView(FormGroupContainerView);
        this.userNameFormGroupView = this.formGroupContainerView.addFormGroupTextView();
        this.appKeyFormGroupView = this.formGroupContainerView.addFormGroupTextView();
        this.modCategoryNameFormGroupView = this.formGroupContainerView.addFormGroupTextView();
        this.modifierNameFormGroupView = this.formGroupContainerView.addFormGroupTextView();
        this.roleNameFormGroupView = this.formGroupContainerView.addFormGroupTextView();
        const buttonContainer = mainContent.addView(ButtonContainerView);
        this.appLink = buttonContainer.addLinkCommand();
        this.appLink.setText('View App');
        this.userLink = buttonContainer.addLinkCommand();
        this.userLink.setText('View User');
        this.deleteUserRoleLink = buttonContainer.addButtonCommand();
        this.deleteUserRoleLink.useOutlineStyle(ContextualClass.danger);
        this.deleteUserRoleLink.setText('Delete User Role');
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.modalConfirmView = this.addView(ModalConfirmView);
        this.modalAlertView = this.addView(ModalMessageAlertView);
    }
}