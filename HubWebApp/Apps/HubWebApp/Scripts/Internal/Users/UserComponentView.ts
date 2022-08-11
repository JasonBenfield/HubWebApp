import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ContextualClass } from "@jasonbenfield/sharedwebapp/ContextualClass";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupGridView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { HubTheme } from "../HubTheme";

export class UserComponentView extends CardView {
    readonly alert: CardAlertView;
    readonly userName: FormGroupTextView;
    readonly fullName: FormGroupTextView;
    readonly email: FormGroupTextView;
    readonly editButton: ButtonCommandView;
    readonly changePasswordButton: ButtonCommandView;

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
        this.changePasswordButton = body.addView(ButtonCommandView);
        this.changePasswordButton.icon.solidStyle('lock');
        this.changePasswordButton.setContext(ContextualClass.primary);
        this.changePasswordButton.setText('Change Password');
    }
}