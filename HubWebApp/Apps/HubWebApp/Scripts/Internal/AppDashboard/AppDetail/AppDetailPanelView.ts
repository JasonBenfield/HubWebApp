import { MarginCss } from '@jasonbenfield/sharedwebapp/MarginCss';
import { CssLengthUnit } from '@jasonbenfield/sharedwebapp/CssLengthUnit';
import { BasicComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicComponentView';
import { ButtonCommandView } from '@jasonbenfield/sharedwebapp/Views/Command';
import { GridView } from '@jasonbenfield/sharedwebapp/Views/Grid';
import { ToolbarView } from '@jasonbenfield/sharedwebapp/Views/ToolbarView';
import { HubTheme } from '../../HubTheme';
import { AppComponentView } from './AppComponentView';
import { CurrentVersionComponentView } from './CurrentVersionComponentView';
import { ModifierCategoryListCardView } from './ModifierCategoryListCardView';
import { MostRecentErrorEventListCardView } from './MostRecentErrorEventListCardView';
import { MostRecentRequestListCardView } from './MostRecentRequestListCardView';
import { ResourceGroupListCardView } from './ResourceGroupListCardView';

export class AppDetailPanelView extends GridView {
    readonly app: AppComponentView;
    readonly currentVersion: CurrentVersionComponentView;
    readonly resourceGroupListCard: ResourceGroupListCardView;
    readonly modifierCategoryListCard: ModifierCategoryListCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;

    readonly backButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.styleAsLayout();
        this.setTemplateRows(CssLengthUnit.flex(1));
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.app = mainContent.addView(AppComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.currentVersion = mainContent.addView(CurrentVersionComponentView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = mainContent.addView(ResourceGroupListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierCategoryListCard = mainContent.addView(ModifierCategoryListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = mainContent.addView(MostRecentRequestListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = mainContent.addView(MostRecentErrorEventListCardView)
            .configure(b => b.setMargin(MarginCss.bottom(3)));

        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.backButton = toolbar.columnStart.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.backButton(this.backButton);
    }
}