import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { WebPage } from '@jasonbenfield/sharedwebapp/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/XtiUrl';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../../Hub/Apis';
import { AppDetailPanel } from './AppDetail/AppDetailPanel';
import { MainPageView } from './MainPageView';
import { ModCategoryPanel } from './ModCategory/ModCategoryPanel';
import { ResourcePanel } from './Resource/ResourcePanel';
import { ResourceGroupPanel } from './ResourceGroup/ResourceGroupPanel';

class MainPage {
    private readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly panels: SingleActivePanel;
    private readonly appDetailPanel: AppDetailPanel;
    private readonly resourceGroupPanel: ResourceGroupPanel;
    private readonly resourcePanel: ResourcePanel;
    private readonly modCategoryPanel: ModCategoryPanel;

    constructor(page: PageFrameView) {
        this.view = new MainPageView(page);
        this.hubApi = new Apis(page.modalError).hub();
        this.appDetailPanel = this.panels.add(new AppDetailPanel(this.hubApi, this.view.appDetailPanel));
        this.resourceGroupPanel = this.panels.add(new ResourceGroupPanel(this.hubApi, this.view.resourceGroupPanel));
        this.resourcePanel = this.panels.add(new ResourcePanel(this.hubApi, this.view.resourcePanel));
        this.modCategoryPanel = this.panels.add(new ModCategoryPanel(this.hubApi, this.view.modCategoryPanel));
        if (XtiUrl.current.path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage(this.hubApi.Apps.Index.getUrl({})).open();
        }
    }

    private async activateAppDetailPanel() {
        this.panels.activate(this.appDetailPanel);
        this.appDetailPanel.refresh();
        let result = await this.appDetailPanel.start();
        if (result.backRequested) {
            this.hubApi.Apps.Index.open({});
        }
        else if (result.resourceGroupSelected) {
            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.ID);
        }
        else if (result.modCategorySelected) {
            this.activateModCategoryPanel(result.modCategorySelected.modCategory.ID);
        }
    }

    private async activateResourceGroupPanel(groupID?: number) {
        this.panels.activate(this.resourceGroupPanel);
        if (groupID) {
            this.resourceGroupPanel.setGroupID(groupID);
        }
        this.resourceGroupPanel.refresh();
        let result = await this.resourceGroupPanel.start();
        if (result.backRequested) {
            this.activateAppDetailPanel();
        }
        else if (result.resourceSelected) {
            this.activateResourcePanel(result.resourceSelected.resource.ID);
        }
        else if (result.modCategorySelected) {
            this.activateModCategoryPanel(result.modCategorySelected.modCategory.ID);
        }
    }

    private async activateResourcePanel(resourceID?: number) {
        this.panels.activate(this.resourcePanel);
        if (resourceID) {
            this.resourcePanel.setResourceID(resourceID);
        }
        this.resourcePanel.refresh();
        let result = await this.resourcePanel.start();
        if (result.backRequested) {
            this.activateResourceGroupPanel();
        }
    }

    private async activateModCategoryPanel(modCategoryID: number) {
        this.panels.activate(this.modCategoryPanel);
        this.modCategoryPanel.setModCategoryID(modCategoryID);
        this.modCategoryPanel.refresh();
        let result = await this.modCategoryPanel.start();
        if (result.backRequested) {
            this.activateAppDetailPanel();
        }
        else if (result.resourceGroupSelected) {
            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.ID);
        }
    }
}
new MainPage(new Startup().build());