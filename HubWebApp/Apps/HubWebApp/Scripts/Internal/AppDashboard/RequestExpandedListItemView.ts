import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpanView } from "@jasonbenfield/sharedwebapp/Html/TextSpanView";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";

export class RequestExpandedListItemView extends ListGroupItemView {
    readonly timeStarted: TextSpanView;
    readonly groupName: TextSpanView;
    readonly actionName: TextSpanView;
    readonly userName: TextSpanView;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.timeStarted = row.addColumn()
            .addContent(new TextSpanView());
        this.groupName = row.addColumn()
            .addContent(new TextSpanView());
        this.actionName = row.addColumn()
            .addContent(new TextSpanView());
        this.userName = row.addColumn()
            .addContent(new TextSpanView());
    }
}