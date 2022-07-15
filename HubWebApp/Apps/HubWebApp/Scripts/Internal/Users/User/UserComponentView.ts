import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Commands";
import { FormGroupGridView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { HubTheme } from "../../HubTheme";

export class UserComponentView extends CardView {
    readonly alert: CardAlertView;
    readonly userName: FormGroupTextView;
    readonly fullName: FormGroupTextView;
    readonly email: FormGroupTextView;
    readonly editButton: ButtonCommandView;

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
        const body = this.addCardBody()
            .addView(FormGroupGridView);
        this.userName = body.addFormGroup(FormGroupTextView);
        this.fullName = body.addFormGroup(FormGroupTextView);
        this.email = body.addFormGroup(FormGroupTextView);
    }
}