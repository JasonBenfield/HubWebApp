import { SingleActivePanel } from "@jasonbenfield/sharedwebapp/Panel/SingleActivePanel";
import { HubPage } from "../HubPage";
import { MainMenuPanel } from "../MainMenuPanel";
import { ConfigureInstallTemplatePanel } from "./ConfigureInstallTemplatePanel";
import { InstallTemplatesPanel } from "./InstallTemplatesPanel";
import { MainPageView } from "./MainPageView";

class MainPage extends HubPage {
    private readonly panels: SingleActivePanel;
    private readonly installTemplatesPanel: InstallTemplatesPanel;
    private readonly configureInstallTemplatePanel: ConfigureInstallTemplatePanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor(protected readonly view: MainPageView) {
        super(view);
        this.panels = new SingleActivePanel();
        this.installTemplatesPanel = this.panels.add(
            new InstallTemplatesPanel(this.hubClient, view.installTemplatePanelView)
        );
        this.configureInstallTemplatePanel = this.panels.add(
            new ConfigureInstallTemplatePanel(this.hubClient, view.configureInstallTemplatePanelView)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        this.installTemplatesPanel.refresh();
        this.activateInstallTemplatesPanel();
    }

    private async activateInstallTemplatesPanel() {
        this.panels.activate(this.installTemplatesPanel);
        const result = await this.installTemplatesPanel.start();
        if (result.mainMenuRequested) {
            this.activateMainMenuPanel();
        }
        else if (result.configureTemplateRequested) {
            this.configureInstallTemplatePanel.setTemplate(result.configureTemplateRequested.template);
            this.activateConfigureInstallTemplatePanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateInstallTemplatesPanel();
        }
    }

    private async activateConfigureInstallTemplatePanel() {
        this.panels.activate(this.configureInstallTemplatePanel);
        const result = await this.configureInstallTemplatePanel.start();
        if (result.cancelled) {
            this.activateInstallTemplatesPanel();
        }
        else if (result.saved) {
            this.installTemplatesPanel.refresh();
            this.activateInstallTemplatesPanel();
        }
    }
}
new MainPage(new MainPageView());