import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ResourceListItemView } from "./ResourceListItemView";

export class ResourceListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly resources: ButtonListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.resources = this.addView(ButtonListGroupView);
        this.resources.setItemViewType(ResourceListItemView);
    }
}