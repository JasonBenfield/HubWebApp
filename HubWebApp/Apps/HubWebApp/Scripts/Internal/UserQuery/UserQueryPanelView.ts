﻿import { CssLengthUnit } from "@jasonbenfield/sharedwebapp/CssLengthUnit";
import { ODataComponentView } from "@jasonbenfield/sharedwebapp/OData/ODataComponentView";
import { PaddingCss } from "@jasonbenfield/sharedwebapp/PaddingCss";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { BlockView } from "@jasonbenfield/sharedwebapp/Views/BlockView";
import { ButtonCommandView } from "@jasonbenfield/sharedwebapp/Views/Command";
import { GridView, LinkGridRowView } from "@jasonbenfield/sharedwebapp/Views/Grid";
import { LinkListGroupView, TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { ToolbarView } from "@jasonbenfield/sharedwebapp/Views/ToolbarView";
import { ODataExpandedUserColumnViewsBuilder } from "../../Lib/Http/ODataExpandedUserColumnsBuilder";
import { HubTheme } from "../HubTheme";

export class UserQueryPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly userGroups: LinkListGroupView<TextLinkListGroupItemView>;
    readonly odataComponent: ODataComponentView;
    readonly columns: ODataExpandedUserColumnViewsBuilder;
    readonly menuButton: ButtonCommandView;
    readonly addButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.styleAsLayout();
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
        const layoutGrid = mainContent.addView(GridView);
        layoutGrid.styleAsLayout();
        layoutGrid.height100();
        layoutGrid.setTemplateColumns(CssLengthUnit.percentage(25), CssLengthUnit.flex(1));
        const cell1 = layoutGrid.addCell()
            .configure(c => {
                c.positionRelative();
            })
            .addView(BlockView)
            .configure(b => {
                b.positionAbsoluteFill();
                b.scrollable();
                b.setPadding(PaddingCss.end(3));
            });
        this.alert = cell1.addView(MessageAlertView);
        this.userGroups = LinkListGroupView.addTo(cell1, TextLinkListGroupItemView);
        this.odataComponent = layoutGrid.addCell()
            .configure(c => c.setPadding(PaddingCss.start(3)))
            .addView(ODataComponentView);
        this.odataComponent.configureDataRow(grid => grid.addRow(LinkGridRowView));
        this.columns = new ODataExpandedUserColumnViewsBuilder();
        const toolbar = HubTheme.instance.commandToolbar.toolbar(
            this.addCell().addView(ToolbarView)
        );
        this.menuButton = HubTheme.instance.commandToolbar.menuButton(
            toolbar.columnStart.addView(ButtonCommandView)
        );
        this.addButton = HubTheme.instance.commandToolbar.addButton(
            toolbar.columnEnd.addView(ButtonCommandView)
        );
    }
}