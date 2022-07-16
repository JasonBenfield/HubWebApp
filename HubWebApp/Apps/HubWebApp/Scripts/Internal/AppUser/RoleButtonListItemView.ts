import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class RoleButtonListItemView extends ButtonListGroupItemView {
    readonly roleName: BasicTextComponentView;

    constructor(container: BasicComponentView) {
        super(container);
        this.roleName = this.addView(TextSpanView);
    }
}