import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { AppDetailPanelView } from './AppDetail/AppDetailPanelView';
import { ModCategoryPanelView } from './ModCategory/ModCategoryPanelView';
import { ResourcePanelView } from './Resource/ResourcePanelView';
import { ResourceGroupPanelView } from './ResourceGroup/ResourceGroupPanelView';

export class MainPageView {
    readonly appDetailPanel: AppDetailPanelView;
    readonly resourceGroupPanel: ResourceGroupPanelView;
    readonly resourcePanel: ResourcePanelView;
    readonly modCategoryPanel: ModCategoryPanelView;

    constructor(private readonly page: PageFrameView) {
        this.page.content.setPadding(PaddingCss.top(3));
        this.appDetailPanel = this.page.addContent(new AppDetailPanelView());
        this.resourceGroupPanel = this.page.addContent(new ResourceGroupPanelView());
        this.resourcePanel = this.page.addContent(new ResourcePanelView());
        this.modCategoryPanel = this.page.addContent(new ModCategoryPanelView());
    }
}