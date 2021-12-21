import { ButtonCommandItem } from '@jasonbenfield/sharedwebapp/Command/ButtonCommandItem';
import { Block } from '@jasonbenfield/sharedwebapp/Html/Block';
import { BlockViewModel } from '@jasonbenfield/sharedwebapp/Html/BlockViewModel';
import { FlexColumn } from '@jasonbenfield/sharedwebapp/Html/FlexColumn';
import { FlexColumnFill } from '@jasonbenfield/sharedwebapp/Html/FlexColumnFill';
import { MarginCss } from '@jasonbenfield/sharedwebapp/MarginCss';
import { HubTheme } from '../../HubTheme';
import { AppComponentView } from './AppComponentView';
import { CurrentVersionComponentView } from './CurrentVersionComponentView';
import { ModifierCategoryListCardView } from './ModifierCategoryListCardView';
import { MostRecentErrorEventListCardView } from './MostRecentErrorEventListCardView';
import { MostRecentRequestListCardView } from './MostRecentRequestListCardView';
import { ResourceGroupListCardView } from './ResourceGroupListCardView';

export class AppDetailPanelView extends Block {
    readonly app: AppComponentView;
    readonly currentVersion: CurrentVersionComponentView;
    readonly resourceGroupListCard: ResourceGroupListCardView;
    readonly modifierCategoryListCard: ModifierCategoryListCardView;
    readonly mostRecentRequestListCard: MostRecentRequestListCardView;
    readonly mostRecentErrorEventListCard: MostRecentErrorEventListCardView;

    readonly backButton: ButtonCommandItem;

    constructor(vm: BlockViewModel = new BlockViewModel()) {
        super(vm);
        this.height100();
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.app = flexFill
            .addContent(new AppComponentView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.currentVersion = flexFill
            .addContent(new CurrentVersionComponentView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.modifierCategoryListCard = flexFill
            .addContent(new ModifierCategoryListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));
        this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCardView())
            .configure(b => b.setMargin(MarginCss.bottom(3)));

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.backButton = toolbar.columnStart.addContent(
            HubTheme.instance.commandToolbar.backButton()
        );
    }
}