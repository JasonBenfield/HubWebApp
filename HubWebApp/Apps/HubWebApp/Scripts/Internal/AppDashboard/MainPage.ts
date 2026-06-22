import { WebPage } from "@jasonbenfield/sharedwebapp/Http/WebPage";
import { XtiUrl } from "@jasonbenfield/sharedwebapp/Http/XtiUrl";
import { SingleActivePanel } from "@jasonbenfield/sharedwebapp/Panel/SingleActivePanel";
import { HubPage } from "../HubPage";
import { AppDetailPanel } from "./AppDetail/AppDetailPanel";
import { MainPageView } from "./MainPageView";
import { ModCategoryPanel } from "./ModCategory/ModCategoryPanel";
import { ResourcePanel } from "./Resource/ResourcePanel";
import { ResourceGroupPanel } from "./ResourceGroup/ResourceGroupPanel";
import { InstallAppPanel } from "./AppDetail/InstallAppPanel";
import { ConfigureInstallPanel } from "./AppDetail/ConfigureInstallPanel";
import { SelectInstallTemplatesPanel } from "./AppDetail/SelectInstallTemplatesPanel";

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly appDetailPanel: AppDetailPanel;
    private readonly resourceGroupPanel: ResourceGroupPanel;
    private readonly resourcePanel: ResourcePanel;
    private readonly modCategoryPanel: ModCategoryPanel;
    private readonly selectInstallTemplatePanel: SelectInstallTemplatesPanel;
    private readonly configureInstallPanel: ConfigureInstallPanel;
    private readonly installAppPanel: InstallAppPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.appDetailPanel = this.panels.add(new AppDetailPanel(this.hubClient, view.appDetailPanel));
        this.resourceGroupPanel = this.panels.add(new ResourceGroupPanel(this.hubClient, view.resourceGroupPanel));
        this.resourcePanel = this.panels.add(new ResourcePanel(this.hubClient, view.resourcePanel));
        this.modCategoryPanel = this.panels.add(new ModCategoryPanel(this.hubClient, view.modCategoryPanel));
        this.selectInstallTemplatePanel = this.panels.add(new SelectInstallTemplatesPanel(this.hubClient, view.selectInstallTemplatePanelView));
        this.configureInstallPanel = this.panels.add(new ConfigureInstallPanel(this.hubClient, view.configureInstallPanelView));
        this.installAppPanel = this.panels.add(new InstallAppPanel(this.hubClient, view.installAppPanelView));
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
        else if (result.configureInstallRequested) {
            if (result.configureInstallRequested.installConfiguration.isFound) {
                this.configureInstallPanel.setInstallConfiguration(
                    result.configureInstallRequested.app,
                    result.configureInstallRequested.installConfigurations,
                    result.configureInstallRequested.installConfiguration
                );
                this.configureInstallPanel.setInstallTemplate(result.configureInstallRequested.installConfiguration.template);
                this.activateConfigureInstallPanel();
            }
            else {
                this.configureInstallPanel.setInstallConfiguration(
                    result.configureInstallRequested.app,
                    result.configureInstallRequested.installConfigurations,
                    result.configureInstallRequested.installConfiguration
                );
                this.selectInstallTemplatePanel.refresh();
                this.activateSelectInstallTemplatePanel(this.appDetailPanel);
            }
        }
        else if (result.installRequested) {
            this.installAppPanel.setApp(result.installRequested.app.appKey, result.installRequested.version.versionKey);
            this.installAppPanel.setInstallConfigurations(result.installRequested.installConfigurations);
            this.activateInstallAppPanel();
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

    private async activateSelectInstallTemplatePanel(returnTo: AppDetailPanel | ConfigureInstallPanel) {
        this.panels.activate(this.selectInstallTemplatePanel);
        const result = await this.selectInstallTemplatePanel.start();
        if (result.back) {
            if (returnTo === this.appDetailPanel) {
                this.activateAppDetailPanel();
            }
            else if (returnTo === this.configureInstallPanel) {
                this.activateConfigureInstallPanel();
            }
        }
        else if (result.templateSelected) {
            this.configureInstallPanel.setInstallTemplate(result.templateSelected.template);
            this.activateConfigureInstallPanel();
        }
    }

    private async activateConfigureInstallPanel() {
        this.panels.activate(this.configureInstallPanel);
        const result = await this.configureInstallPanel.start();
        if (result.cancelled) {
            this.activateAppDetailPanel();
        }
        else if (result.saved) {
            this.appDetailPanel.refresh();
            this.activateAppDetailPanel();
        }
        else if (result.selectTemplateRequested) {
            this.selectInstallTemplatePanel.refresh();
            this.activateSelectInstallTemplatePanel(this.configureInstallPanel);
        }
    }

    private async activateInstallAppPanel() {
        this.installAppPanel.start();
        const result = await this.installAppPanel.start();
        if (result.back) {
            this.activateAppDetailPanel();
        }
    }
}
new MainPage(new MainPageView());