import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";
import { InstallTemplateListItemView } from "./InstallTemplateListItemView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";

export class InstallTemplatesPanelView extends GridView {
    readonly cardAlertView: CardAlertView;
    readonly templateListView: GridListGroupView<InstallTemplateListItemView>;
    readonly menuButton: ButtonCommandView;
    readonly refreshButton: ButtonCommandView;
    readonly addButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const cardView = mainContent.addView(CardView);
        cardView.setMargin(MarginCss.bottom(3));
        const titleTextView = cardView.addCardHeader().addView(TextHeading3View);
        titleTextView.addCssName("card-title");
        titleTextView.setText("Install Templates");
        this.cardAlertView = cardView.addCardAlert();
        this.templateListView = cardView.addGridListGroup(InstallTemplateListItemView);
        InstallTemplateListItemView.setTemplateColumns(this.templateListView);
        this.templateListView.setHeaderViewType(InstallTemplateListItemView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.menuButton.setMargin(MarginCss.end(1));
        this.refreshButton = HubTheme.instance.commandToolbar.refreshButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.addButton = HubTheme.instance.commandToolbar.addButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }
}