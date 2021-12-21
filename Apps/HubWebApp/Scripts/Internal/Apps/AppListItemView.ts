import { Row } from "@jasonbenfield/sharedwebapp/Grid/Row";
import { TextSpan } from "@jasonbenfield/sharedwebapp/Html/TextSpan";
import { LinkListGroupItemView } from "@jasonbenfield/sharedwebapp/ListGroup/LinkListGroupItemView";
import { LinkListItemViewModel } from "@jasonbenfield/sharedwebapp/ListGroup/LinkListItemViewModel";

export class AppListItemView extends LinkListGroupItemView {
    private readonly appName: TextSpan;
    private readonly appTitle: TextSpan;
    private readonly appType: TextSpan;

    constructor() {
        super(new LinkListItemViewModel());
        let row = this.addContent(new Row());
        this.appName = row.addColumn()
            .addContent(new TextSpan());
        this.appTitle = row.addColumn()
            .addContent(new TextSpan());
        this.appType = row.addColumn()
            .addContent(new TextSpan());
    }

    setAppName(appName: string) { this.appName.setText(appName); }

    setAppTitle(appTitle: string) { this.appTitle.setText(appTitle); }

    setAppType(displayText: string) { this.appType.setText(displayText); }
}