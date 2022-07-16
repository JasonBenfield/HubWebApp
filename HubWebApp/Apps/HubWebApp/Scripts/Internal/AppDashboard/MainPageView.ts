import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { AppDetailPanelView } from './AppDetail/AppDetailPanelView';
import { ModCategoryPanelView } from './ModCategory/ModCategoryPanelView';
import { ResourcePanelView } from './Resource/ResourcePanelView';
import { ResourceGroupPanelView } from './ResourceGroup/ResourceGroupPanelView';

export class MainPageView extends BasicPageView {
    readonly appDetailPanel: AppDetailPanelView;
    readonly resourceGroupPanel: ResourceGroupPanelView;
    readonly resourcePanel: ResourcePanelView;
    readonly modCategoryPanel: ModCategoryPanelView;

    constructor() {
        super();
        this.appDetailPanel = this.addView(AppDetailPanelView);
        this.resourceGroupPanel = this.addView(ResourceGroupPanelView);
        this.resourcePanel = this.addView(ResourcePanelView);
        this.modCategoryPanel = this.addView(ModCategoryPanelView);
    }
}