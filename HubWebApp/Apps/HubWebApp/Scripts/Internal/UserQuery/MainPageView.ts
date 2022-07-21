import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { CssLengthUnit } from '@jasonbenfield/sharedwebapp/CssLengthUnit';
import { GridView } from '@jasonbenfield/sharedwebapp/Views/Grid';
import { LinkListGroupView, TextLinkListGroupItemView } from '@jasonbenfield/sharedwebapp/Views/ListGroup';
import { ODataComponentView } from '@jasonbenfield/sharedwebapp/OData/ODataComponentView';
import { ODataExpandedUserColumnViewsBuilder } from '../../Lib/Api/ODataExpandedUserColumnsBuilder';
import { MessageAlertView } from '@jasonbenfield/sharedwebapp/Views/MessageAlertView';
import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';

export class MainPageView extends BasicPageView {
    readonly alert: MessageAlertView;
    readonly userGroups: LinkListGroupView;
    readonly odataComponent: ODataComponentView;
    readonly columns: ODataExpandedUserColumnViewsBuilder;

    constructor() {
        super();
        const layoutGrid = this.addView(GridView);
        layoutGrid.layout();
        layoutGrid.height100();
        layoutGrid.setTemplateColumns(CssLengthUnit.auto(), CssLengthUnit.flex(1));
        const cell1 = layoutGrid.addCell();
        cell1.setPadding(PaddingCss.xs(3));
        this.alert = cell1.addView(MessageAlertView);        
        this.userGroups = cell1
            .addView(LinkListGroupView);
        this.userGroups.setItemViewType(TextLinkListGroupItemView);
        this.odataComponent = layoutGrid.addCell()
            .configure(c => c.setPadding(PaddingCss.xs(3)))
            .addView(ODataComponentView);
        this.odataComponent.configureDataRow(row => row.addCssName('clickable'));
        this.columns = new ODataExpandedUserColumnViewsBuilder();
    }
}