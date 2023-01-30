import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupView, TextListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";

export class UserAuthenticatorListCardView extends CardView {
    readonly alert: CardAlertView;
    readonly userAuthenticators: ListGroupView<TextListGroupItemView>;

    constructor(container: BasicComponentView) {
        super(container);
        const title = this.addCardTitleHeader();
        title.setText('Authenticators');
        this.alert = this.addCardAlert();
        this.userAuthenticators = this.addListGroup(TextListGroupItemView);
    }
}