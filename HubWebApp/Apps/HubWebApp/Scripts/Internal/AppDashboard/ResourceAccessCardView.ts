﻿import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class ResourceAccessCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly accessItems: ListGroupView<RoleAccessListItemView>;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.accessItems = ListGroupView.addTo(this, RoleAccessListItemView);
    }
}