import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { CardAlertView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { RowView } from "@jasonbenfield/sharedwebapp/Views/RowView";
import { TextHeading3View } from "@jasonbenfield/sharedwebapp/Views/TextHeadings";
import { HubTheme } from "../../HubTheme";
import { GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";

export class InstallConfigurationListCardView extends CardView {
    readonly addButton: ButtonCommandView;
    readonly cardAlertView: CardAlertView;
    readonly configurationListView: GridListGroupView<InstallConfigurationListItemView>;

    constructor(container: BasicComponentView) {
        super(container);
        const headerView = this.addCardHeader();
        const rowView = headerView.addView(RowView);
        const col1 = rowView.addColumn();
        const titleTextView = col1.addView(TextHeading3View);
        titleTextView.setText("Install Configurations");
        titleTextView.addCssName("card-title");
        const col2 = rowView.addColumn();
        col2.setColumnCss(ColumnCss.xs("auto"));
        this.addButton = HubTheme.instance.cardHeader.addButton(col2.addView(ButtonCommandView));
        this.addButton.setTitle("Add Install Configuration");
        this.cardAlertView = this.addCardAlert();
        this.configurationListView = this.addGridListGroup(InstallConfigurationListItemView);
        InstallConfigurationListItemView.setTemplateColumns(this.configurationListView);
        this.configurationListView.addCssName("clickable");

    }
}