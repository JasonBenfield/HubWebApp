import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { LinkListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/LinkListGroupItemView";
import { LinkListItemViewModel } from "@jasonbenfield/sharedwebapp/ListGroup/LinkListItemViewModel";

export class AppListItemView extends LinkListGroupItemView {
    readonly appName: TextSpanView;
    readonly appTitle: TextSpanView;
    readonly appType: TextSpanView;

    constructor() {
        super(new LinkListItemViewModel());
        let row = this.addContent(new Row());
        this.appName = row.addColumn()
            .addContent(new TextSpanView());
        this.appTitle = row.addColumn()
            .addContent(new TextSpanView());
        this.appType = row.addColumn()
            .addContent(new TextSpanView());
    }
}