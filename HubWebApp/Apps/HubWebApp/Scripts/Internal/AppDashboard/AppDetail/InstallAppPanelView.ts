import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { MarginCss } from "@jasonbenfield/sharedwebapp/MarginCss";
import { FormView } from "@jasonbenfield/sharedwebapp/Views/FormView";
import { FormGroupInputView, FormGroupTextView } from "@jasonbenfield/sharedwebapp/Views/FormGroup";
import { ModalErrorView } from "@jasonbenfield/sharedwebapp/Views/ModalError";
import { HubTheme } from "../../HubTheme";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";
import { GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";

export class InstallAppPanelView extends GridView {
    readonly cardAlertView: CardAlertView;
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
        titleTextView.setText("Select Install Configuration");
        this.cardAlertView = cardView.addCardAlert();
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