import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ButtonListGroupView, TextButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../HubTheme";

export class EditUserGroupPanelView extends GridView {
    readonly alertView: MessageAlertView;
    readonly userGroupListView: ButtonListGroupView<TextButtonListGroupItemView>;
    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        const titleTextView = mainContent.addView(TextHeading3View);
        titleTextView.setText("Select User Group");
        this.alertView = mainContent.addView(MessageAlertView);
        this.userGroupListView = ButtonListGroupView.addTo(mainContent, TextButtonListGroupItemView);
        const toolbar = HubTheme.instance.commandToolbar.toolbar(this.addView(ToolbarView));
        this.backButton = HubTheme.instance.commandToolbar.backButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }
}