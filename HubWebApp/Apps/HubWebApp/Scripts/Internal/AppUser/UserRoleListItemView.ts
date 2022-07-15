import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Commands";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { BasicTextComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicTextComponentView";
import { HubTheme } from "../HubTheme";

export class UserRoleListItemView extends ListGroupItemView {
    readonly roleName: BasicTextComponentView;
    readonly deleteButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        const row = this.addView(RowView);
        const col1 = row.addColumn();
        const roleName = col1.addView(TextBlockView);
        roleName.setPadding(PaddingCss.top(1));
        this.roleName = roleName;
        const col2 = row.addColumn();
        col2.setColumnCss(ColumnCss.xs('auto'));
        this.deleteButton = col2.addView(ButtonCommandView);
        HubTheme.instance.listItem.deleteButton(this.deleteButton);
    }
}