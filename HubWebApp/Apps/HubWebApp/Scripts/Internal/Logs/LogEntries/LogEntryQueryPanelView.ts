import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { ODataComponentView } from "@jasonbenfield/sharedwebapp/OData/ODataComponentView";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { HubTheme } from "../../HubTheme";
import { ODataExpandedLogEntryColumnViewsBuilder } from '../../../Lib/Api/ODataExpandedLogEntryColumnsBuilder';

export class LogEntryQueryPanelView extends GridView {
    readonly menuButton: ButtonCommandView;
    readonly odataComponent: ODataComponentView;
    readonly columns: ODataExpandedLogEntryColumnViewsBuilder;

    constructor(container: BasicComponentView) {
        super(container);
        this.layout();
        this.height100();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = this.addCell()
            .configure(c => c.positionRelative())
            .addView(BlockView)
            .configure(b => {
                b.positionAbsoluteFill();
            })
            .addView(BlockView)
            .configure(b => {
                b.addCssName('container');
                b.height100();
                b.setPadding(PaddingCss.top(3));
            });
        this.odataComponent = mainContent.addView(ODataComponentView);
        this.odataComponent.configureDataRow(row => row.addCssName('clickable'));
        this.columns = new ODataExpandedLogEntryColumnViewsBuilder();
        this.columns.TimeOccurred.setWidth(CssLengthUnit.maxContent());
        this.columns.SeverityText.setWidth(CssLengthUnit.maxContent());
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
    }
}