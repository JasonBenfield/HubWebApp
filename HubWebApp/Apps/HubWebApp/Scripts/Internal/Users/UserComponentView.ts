import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonGroupView } from "@jasonbenfield/sharedwebapp/Views/ButtonGroupView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { HubTheme } from "../HubTheme";

export class UserComponentView extends CardView {
    readonly alert: CardAlertView;
    readonly userName: FormGroupTextView;
    readonly fullName: FormGroupTextView;
    readonly email: FormGroupTextView;
    readonly timeDeactivated: FormGroupTextView;
    readonly editButton: ButtonCommandView;
    readonly changePasswordButton: ButtonCommandView;
    readonly deactivateButton: ButtonCommandView;
    readonly reactivateButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(UserComponentView.name);
        const headerRow = this.addCardHeader().addView(RowView);
        headerRow.addColumn()
            .addView(TextSpanView)
            .configure(ts => ts.setText('User'));
        this.editButton = headerRow.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(ButtonCommandView)
            .configure(b => HubTheme.instance.cardHeader.editButton(b));
        this.alert = this.addCardAlert();
        const body = this.addCardBody();
        const formGroupGrid = body.addView(FormGroupGridView);
        this.userName = formGroupGrid.addFormGroup(FormGroupTextView);
        this.fullName = formGroupGrid.addFormGroup(FormGroupTextView);
        this.email = formGroupGrid.addFormGroup(FormGroupTextView);
        this.timeDeactivated = formGroupGrid.addFormGroup(FormGroupTextView);
        const btnGroup = body.addView(BlockView);
        btnGroup.addCssName('d-grid');
        btnGroup.addCssName('gap-2');
        btnGroup.addCssName('col-6');
        btnGroup.addCssName('mx-auto');
        this.changePasswordButton = btnGroup.addView(ButtonCommandView);
        this.changePasswordButton.icon.solidStyle('lock');
        this.changePasswordButton.useOutlineStyle(ContextualClass.primary);
        this.changePasswordButton.setText('Change Password');
        this.deactivateButton = btnGroup.addView(ButtonCommandView);
        this.deactivateButton.icon.solidStyle('times');
        this.deactivateButton.useOutlineStyle(ContextualClass.danger);
        this.deactivateButton.setText('Deactivate');
        this.reactivateButton = btnGroup.addView(ButtonCommandView);
        this.reactivateButton.icon.solidStyle('check');
        this.reactivateButton.useOutlineStyle(ContextualClass.primary);
        this.reactivateButton.setText('Reactivate');
    }
}