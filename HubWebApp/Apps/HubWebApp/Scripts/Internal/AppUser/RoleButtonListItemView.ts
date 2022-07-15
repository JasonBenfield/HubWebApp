import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicTextComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicTextComponentView";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";

export class RoleButtonListItemView extends ButtonListGroupItemView {
    readonly roleName: BasicTextComponentView;

    constructor(container: BasicComponentView) {
        super(container);
        this.roleName = this.addView(TextSpanView);
    }
}