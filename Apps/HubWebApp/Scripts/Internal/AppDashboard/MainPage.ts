import { Startup } from 'xtistart';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { XtiUrl } from 'XtiShared/XtiUrl';
import { WebPage } from 'XtiShared/WebPage';
import { SingleActivePanel } from '../Panel/SingleActivePanel';
import { AppDetailPanel } from './AppDetail/AppDetailPanel';
import { ResourceGroupPanel } from './ResourceGroup/ResourceGroupPanel';
import { ResourcePanel } from './Resource/ResourcePanel';
import { ModCategoryPanel } from './ModCategory/ModCategoryPanel';
import { PageFrame } from 'XtiShared/PageFrame';
import { Panel } from '../Panel/Panel';
import { PaddingCss } from 'XtiShared/PaddingCss';

class MainPage {
    constructor(private readonly page: PageFrame) {
        this.page.content.setPadding(PaddingCss.top(3));
        this.hubApi = this.page.api<HubAppApi>(HubAppApi);
        this.appDetailPanel = this.page.addContent(
            this.panels.add(new AppDetailPanel(this.hubApi))
        );
        this.resourceGroupPanel = this.page.addContent(
            this.panels.add(new ResourceGroupPanel(this.hubApi))
        );
        this.resourcePanel = this.page.addContent(
            this.panels.add(new ResourcePanel(this.hubApi))
        );
        this.modCategoryPanel = this.page.addContent(
            this.panels.add(new ModCategoryPanel(this.hubApi))
        );
        if (XtiUrl.current.path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage(this.hubApi.Apps.Index.getUrl({})).open();
        }
    }

    private readonly hubApi: HubAppApi;
    private readonly panels = new SingleActivePanel();
    private readonly appDetailPanel: Panel<AppDetailPanel>;
    private readonly resourceGroupPanel: Panel<ResourceGroupPanel>;
    private readonly resourcePanel: Panel<ResourcePanel>;
    private readonly modCategoryPanel: Panel<ModCategoryPanel>;

    private async activateAppDetailPanel() {
        this.panels.activate(this.appDetailPanel);
        this.appDetailPanel.content.refresh();
        let result = await this.appDetailPanel.content.start();
        if (result.key === AppDetailPanel.ResultKeys.backRequested) {
            this.hubApi.Apps.Index.open({});
        }
        else if (result.key === AppDetailPanel.ResultKeys.resourceGroupSelected) {
            let resourceGroup: IResourceGroupModel = result.data;
            this.activateResourceGroupPanel(resourceGroup.ID);
        }
        else if (result.key === AppDetailPanel.ResultKeys.modCategorySelected) {
            let modCategory: IModifierCategoryModel = result.data;
            this.activateModCategoryPanel(modCategory.ID);
        }
    }

    private async activateResourceGroupPanel(groupID?: number) {
        this.panels.activate(this.resourceGroupPanel);
        if (groupID) {
            this.resourceGroupPanel.content.setGroupID(groupID);
        }
        this.resourceGroupPanel.content.refresh();
        let result = await this.resourceGroupPanel.content.start();
        if (result.key === ResourceGroupPanel.ResultKeys.backRequested) {
            this.activateAppDetailPanel();
        }
        else if (result.key === ResourceGroupPanel.ResultKeys.resourceSelected) {
            let resource: IResourceModel = result.data;
            this.activateResourcePanel(resource.ID);
        }
        else if (result.key === ResourceGroupPanel.ResultKeys.modCategorySelected) {
            let modCategory: IModifierCategoryModel = result.data;
            this.activateModCategoryPanel(modCategory.ID);
        }
    }

    private async activateResourcePanel(resourceID?: number) {
        this.panels.activate(this.resourcePanel);
        if (resourceID) {
            this.resourcePanel.content.setResourceID(resourceID);
        }
        this.resourcePanel.content.refresh();
        let result = await this.resourcePanel.content.start();
        if (result.key === ResourcePanel.ResultKeys.backRequested) {
            this.activateResourceGroupPanel();
        }
    }

    private async activateModCategoryPanel(modCategoryID: number) {
        this.panels.activate(this.modCategoryPanel);
        this.modCategoryPanel.content.setModCategoryID(modCategoryID);
        this.modCategoryPanel.content.refresh();
        let result = await this.modCategoryPanel.content.start();
        if (result.key === ModCategoryPanel.ResultKeys.backRequested) {
            this.activateAppDetailPanel();
        }
        else if (result.key === ModCategoryPanel.ResultKeys.resourceGroupSelected) {
            let resourceGroup: IResourceGroupModel = result.data;
            this.activateResourceGroupPanel(resourceGroup.ID);
        }
    }
}
new MainPage(new Startup().build());