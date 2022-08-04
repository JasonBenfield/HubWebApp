import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Views/TextSpanView";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";

export class ResourceComponentView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly resourceName: TextSpanView;
    readonly resultType: TextSpanView;
    private readonly anonListItem: ListGroupItemView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        const listGroup = this.addUnorderedListGroup();
        const listItem = listGroup.addListGroupItem();
        const row = listItem.addView(RowView);
        this.resourceName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addView(TextSpanView);
        this.resultType = row.addColumn()
            .addView(TextSpanView);
        this.anonListItem = listGroup.addListGroupItem();
        this.anonListItem.addView(RowView)
            .addColumn()
            .addView(TextSpanView)
            .configure(ts => ts.setText('Anonymous is Allowed'));
        this.anonListItem.hide();
    }

    showAnon() {
        this.anonListItem.show();
    }

    hideAnon() {
        this.anonListItem.hide();
    }
}