import { WebPage } from '@jasonbenfield/sharedwebapp/Http/WebPage';
import { XtiUrl } from '@jasonbenfield/sharedwebapp/Http/XtiUrl';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { AppDetailPanel } from './AppDetail/AppDetailPanel';
import { MainPageView } from './MainPageView';
import { ModCategoryPanel } from './ModCategory/ModCategoryPanel';
import { ResourcePanel } from './Resource/ResourcePanel';
import { ResourceGroupPanel } from './ResourceGroup/ResourceGroupPanel';

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly appDetailPanel: AppDetailPanel;
    private readonly resourceGroupPanel: ResourceGroupPanel;
    private readonly resourcePanel: ResourcePanel;
    private readonly modCategoryPanel: ModCategoryPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.appDetailPanel = this.panels.add(new AppDetailPanel(this.hubClient, this.view.appDetailPanel));
        this.resourceGroupPanel = this.panels.add(new ResourceGroupPanel(this.hubClient, this.view.resourceGroupPanel));
        this.resourcePanel = this.panels.add(new ResourcePanel(this.hubClient, this.view.resourcePanel));
        this.modCategoryPanel = this.panels.add(new ModCategoryPanel(this.hubClient, this.view.modCategoryPanel));
        if (XtiUrl.current().path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage(this.hubClient.Apps.Index.getUrl({})).open();
        }
    }

    private async activateAppDetailPanel() {
        this.panels.activate(this.appDetailPanel);
        this.appDetailPanel.refresh();
        const result = await this.appDetailPanel.start();
        if (result.backRequested) {
            this.hubClient.Apps.Index.open({});
        }
        else if (result.resourceGroupSelected) {
            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.id);
        }
        else if (result.modCategorySelected) {
            this.activateModCategoryPanel(result.modCategorySelected.modCategory.id);
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
            this.activateResourcePanel(result.resourceSelected.resource.id);
        }
        else if (result.modCategorySelected) {
            this.activateModCategoryPanel(result.modCategorySelected.modCategory.id);
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
            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.id);
        }
    }
}
new MainPage(new MainPageView());