import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BasicTextComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicTextComponentView";
import { GridListGroupItemView, GridListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { TextBlockView } from "@jasonbenfield/sharedwebapp/Views/TextBlockView";

export class InstallConfigurationListItemView extends GridListGroupItemView {
    static setTemplateColumns(listView: GridListGroupView<InstallConfigurationListItemView>) {
        listView.setTemplateColumns(
            CssLengthUnit.auto(),
            CssLengthUnit.auto(),
            CssLengthUnit.auto(),
            CssLengthUnit.auto(),
            CssLengthUnit.flex(1)
        );
    }

    readonly templateNameTextView: BasicTextComponentView;
    readonly machineNameTextView: BasicTextComponentView;
    readonly domainTextView: BasicTextComponentView;
    readonly siteNameTextView: BasicTextComponentView;
    readonly configurationNameTextView: BasicTextComponentView;

    constructor(container: BasicComponentView) {
        super(container);
        const cell1 = this.addCell();
        this.templateNameTextView = cell1.addView(TextBlockView);
        const cell2 = this.addCell();
        this.machineNameTextView = cell2.addView(TextBlockView);
        const cell3 = this.addCell();
        this.domainTextView = cell3.addView(TextBlockView);
        const cell4 = this.addCell();
        this.siteNameTextView = cell4.addView(TextBlockView);
        const cell5 = this.addCell();
        this.configurationNameTextView = cell5.addView(TextBlockView);
    }
}