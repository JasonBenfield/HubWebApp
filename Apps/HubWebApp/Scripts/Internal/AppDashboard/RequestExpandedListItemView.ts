import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { ListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView";

export class RequestExpandedListItemView extends ListGroupItemView {
    private readonly timeStarted: TextSpan;
    private readonly groupName: TextSpan;
    private readonly actionName: TextSpan;
    private readonly userName: TextSpan;

    constructor() {
        super();
        let row = this.addContent(new Row());
        this.timeStarted = row.addColumn()
            .addContent(new TextSpan());
        this.groupName = row.addColumn()
            .addContent(new TextSpan());
        this.actionName = row.addColumn()
            .addContent(new TextSpan());
        this.userName = row.addColumn()
            .addContent(new TextSpan());
    }

    setTimeStarted(timeStarted: string) { this.timeStarted.setText(timeStarted); }

    setGroupName(groupName: string) { this.groupName.setText(groupName); }

    setActionName(actionName: string) { this.actionName.setText(actionName); }

    setUserName(userName: string) { this.userName.setText(userName); }
}