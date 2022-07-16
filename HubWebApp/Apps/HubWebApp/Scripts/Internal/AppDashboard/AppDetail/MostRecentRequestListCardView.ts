import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { RequestExpandedListItemView } from "../RequestExpandedListItemView";

export class MostRecentRequestListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly requests: ListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.requests = this.addView(ListGroupView);
        this.requests.setItemViewType(RequestExpandedListItemView);
    }
}