import { HubPageView } from '../HubPageView';
import { AppDetailPanelView } from './AppDetail/AppDetailPanelView';
import { ConfigureInstallPanelView } from './AppDetail/ConfigureInstallPanelView';
import { InstallAppPanelView } from './AppDetail/InstallAppPanelView';
import { SelectInstallTemplatesPanelView } from './AppDetail/SelectInstallTemplatesPanelView';
import { ModCategoryPanelView } from './ModCategory/ModCategoryPanelView';
import { ResourcePanelView } from './Resource/ResourcePanelView';
import { ResourceGroupPanelView } from './ResourceGroup/ResourceGroupPanelView';

export class MainPageView extends HubPageView {
    readonly appDetailPanel: AppDetailPanelView;
    readonly resourceGroupPanel: ResourceGroupPanelView;
    readonly resourcePanel: ResourcePanelView;
    readonly modCategoryPanel: ModCategoryPanelView;
    readonly selectInstallTemplatePanelView: SelectInstallTemplatesPanelView;
    readonly configureInstallPanelView: ConfigureInstallPanelView;
    readonly installAppPanelView: InstallAppPanelView;

    constructor() {
        super();
        this.appDetailPanel = this.addView(AppDetailPanelView);
        this.resourceGroupPanel = this.addView(ResourceGroupPanelView);
        this.resourcePanel = this.addView(ResourcePanelView);
        this.modCategoryPanel = this.addView(ModCategoryPanelView);
        this.selectInstallTemplatePanelView = this.addView(SelectInstallTemplatesPanelView);
        this.configureInstallPanelView = this.addView(ConfigureInstallPanelView);
        this.installAppPanelView = this.addView(InstallAppPanelView);
    }
}