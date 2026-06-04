import { HubPageView } from "../HubPageView";
import { MainMenuPanelView } from "../MainMenuPanelView";
import { ConfigureInstallTemplatePanelView } from "./ConfigureInstallTemplatePanelView";
import { InstallTemplatesPanelView } from "./InstallTemplatesPanelView";

export class MainPageView extends HubPageView {
    readonly installTemplatePanelView: InstallTemplatesPanelView;
    readonly configureInstallTemplatePanelView: ConfigureInstallTemplatePanelView;
    readonly mainMenuPanel: MainMenuPanelView;

    constructor() {
        super();
        this.installTemplatePanelView = this.addView(InstallTemplatesPanelView);
        this.configureInstallTemplatePanelView = this.addView(ConfigureInstallTemplatePanelView);
        this.mainMenuPanel = this.addView(MainMenuPanelView);
    }
}