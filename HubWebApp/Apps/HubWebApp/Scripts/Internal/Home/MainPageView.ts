import { CssLengthUnit } from '@jasonbenfield/sharedwebapp/CssLengthUnit';
import { FlexCss } from '@jasonbenfield/sharedwebapp/FlexCss';
import { MarginCss } from '@jasonbenfield/sharedwebapp/MarginCss';
import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { GridView } from '@jasonbenfield/sharedwebapp/Views/Grid';
import { NavView } from '@jasonbenfield/sharedwebapp/Views/NavView';
import { TextHeading1View } from '@jasonbenfield/sharedwebapp/Views/TextHeadings';
import { TextLinkView } from '@jasonbenfield/sharedwebapp/Views/TextLinkView';
import { HubTheme } from '../HubTheme';

export class MainPageView extends BasicPageView {
    readonly heading: TextHeading1View;
    readonly employeesLink: TextLinkView;
    readonly jobCandidateEmploymentsLink: TextLinkView;
    readonly storesLink: TextLinkView;
    readonly salesPeopleLink: TextLinkView;

    constructor() {
        super();
        const layoutGrid = this.addView(GridView);
        layoutGrid.layout();
        layoutGrid.setTemplateRows(CssLengthUnit.auto(), CssLengthUnit.flex(1));
        layoutGrid.height100();
        this.heading = layoutGrid
            .addCell()
            .configure(b => b.addCssName('container'))
            .addView(TextHeading1View);
        this.heading.setMargin(MarginCss.bottom(3));
        const mainContent = layoutGrid.addCell();
        HubTheme.instance.mainContent(mainContent);
        const navView = mainContent.addView(NavView);
        navView.pills();
        navView.setFlexCss(new FlexCss().column());
    }
}