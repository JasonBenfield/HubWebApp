﻿import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { EventListItemView } from "./EventListItemView";

export class MostRecentErrorEventListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly errorEvents: ListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.errorEvents = this.addView(ListGroupView);
        this.errorEvents.setItemViewType(EventListItemView);
    }
}