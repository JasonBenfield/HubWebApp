import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView";

export class RoleButtonListItemView extends ButtonListGroupItemView {
    readonly roleName: ITextComponentView;

    constructor() {
        super();
        this.roleName = this.addContent(new TextSpanView());
    }
}