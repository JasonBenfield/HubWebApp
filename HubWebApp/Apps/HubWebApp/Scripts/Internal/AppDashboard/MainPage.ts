import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/Api/XtiUrl';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { AppDetailPanel } from './AppDetail/AppDetailPanel';
import { MainPageView } from './MainPageView';
import { ModCategoryPanel } from './ModCategory/ModCategoryPanel';
import { ResourcePanel } from './Resource/ResourcePanel';
import { ResourceGroupPanel } from './ResourceGroup/ResourceGroupPanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly panels: SingleActivePanel;
    private readonly appDetailPanel: AppDetailPanel;
    private readonly resourceGroupPanel: ResourceGroupPanel;
    private readonly resourcePanel: ResourcePanel;
    private readonly modCategoryPanel: ModCategoryPanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.panels = new SingleActivePanel();
        this.appDetailPanel = this.panels.add(new AppDetailPanel(this.hubApi, this.view.appDetailPanel));
        this.resourceGroupPanel = this.panels.add(new ResourceGroupPanel(this.hubApi, this.view.resourceGroupPanel));
        this.resourcePanel = this.panels.add(new ResourcePanel(this.hubApi, this.view.resourcePanel));
        this.modCategoryPanel = this.panels.add(new ModCategoryPanel(this.hubApi, this.view.modCategoryPanel));
        if (XtiUrl.current().path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage(this.hubApi.Apps.Index.getUrl({})).open();
        }
    }

    private async activateAppDetailPanel() {
        this.panels.activate(this.appDetailPanel);
        this.appDetailPanel.refresh();
        const result = await this.appDetailPanel.start();
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
        const result = await this.resourceGroupPanel.start();
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
new MainPage();