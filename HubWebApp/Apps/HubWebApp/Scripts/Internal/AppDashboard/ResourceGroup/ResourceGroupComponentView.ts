import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";

export class ResourceGroupComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly groupName: TextSpanView;
    private readonly anonListItem: ListGroupItemView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        const listGroup = this.addUnorderedListGroup(ListGroupItemView);
        const listItem = listGroup.addListGroupItem();
        const row = listItem.addView(RowView);
        this.groupName = row.addColumn()
            .addView(TextSpanView);
        this.anonListItem = listGroup.addListGroupItem();
        this.anonListItem.addView(RowView)
            .addColumn()
            .addView(TextSpanView)
            .configure(ts => ts.setText('Anonymous is Allowed'));
    }

    showAnonMessage() {
        this.anonListItem.show();
    }

    hideAnonMessage() {
        this.anonListItem.hide();
    }
}