import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { FormGroupContainerView } from "@jasonbenfield/sharedwebapp/Views/FormGroupContainerView";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";

export class InstallAppPanelView extends GridView {
    readonly cardAlertView: CardAlertView;
    readonly appKeyFormGroupView: FormGroupTextView;
    readonly versionKeyFormGroupView: FormGroupTextView;
    readonly configurationListView: GridListGroupView<InstallConfigurationListItemView>;
    readonly backButton: ButtonCommandView;

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
        titleTextView.setText("Install");
        this.cardAlertView = cardView.addCardAlert();
        const cardBodyView = cardView.addCardBody();
        const formGroupContainerView = cardBodyView.addView(FormGroupContainerView);
        this.appKeyFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.appKeyFormGroupView.valueTextView.styleAsUserSelectAll();
        this.versionKeyFormGroupView = formGroupContainerView.addFormGroupTextView();
        this.versionKeyFormGroupView.valueTextView.styleAsUserSelectAll();
        this.configurationListView = cardView.addGridListGroup(InstallConfigurationListItemView);
        InstallConfigurationListItemView.setTemplateColumns(this.configurationListView);
        this.configurationListView.addCssName("clickable");
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.backButton = HubTheme.instance.commandToolbar.backButton(
            toolbar.addButtonCommandToStart()
        );
    }
}