import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { XtiUrl } from "@jasonbenfield/sharedwebapp/Http/XtiUrl";
import { App } from "../../../Lib/App";
import { AppResourceGroup } from "../../../Lib/AppResourceGroup";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { HubPermissions, IAppPermissions } from "../../../Lib/HubPermissions";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";
import { ModifierCategory } from "../../../Lib/ModifierCategory";
import { AppComponent } from "./AppComponent";
import { AppDetailPanelView } from "./AppDetailPanelView";
import { CurrentVersionComponent } from "./CurrentVersionComponent";
import { ConfigureInstallEventArgs, InstallConfigurationListCard } from "./InstallConfigurationListCard";
import { ModifierCategoryListCard } from "./ModifierCategoryListCard";
import { MostRecentErrorEventListCard } from "./MostRecentErrorEventListCard";
import { MostRecentRequestListCard } from "./MostRecentRequestListCard";
import { ResourceGroupListCard } from "./ResourceGroupListCard";
import { XtiVersion } from "../../../Lib/XtiVersion";

interface IResult {
    backRequested?: {};
    resourceGroupSelected?: { resourceGroup: AppResourceGroup; };
    modCategorySelected?: { modCategory: ModifierCategory; };
    configureInstallRequested?: {
        app: App,
        installConfigurations: InstallConfiguration[],
        installConfiguration: InstallConfiguration
    };
    installRequested?: {
        app: App,
        version: XtiVersion,
        installConfigurations: InstallConfiguration[]
    };
}

class Result {
    static backRequested() {
        return new Result({ backRequested: {} });
    }

    static resourceGroupSelected(resourceGroup: AppResourceGroup) {
        return new Result({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    }

    static modCategorySelected(modCategory: ModifierCategory) {
        return new Result({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    static configureInstallRequested(app: App, installConfigurations: InstallConfiguration[], installConfiguration: InstallConfiguration) {
        return new Result({
            configureInstallRequested: {
                app: app,
                installConfigurations: installConfigurations,
                installConfiguration: installConfiguration
            }
        });
    }

    static installRequested(app: App, version: XtiVersion, installConfigurations: InstallConfiguration[]) {
        return new Result({
            installRequested: {
                app: app,
                version: version,
                installConfigurations: installConfigurations
            }
        });
    }

    private constructor(private readonly results: IResult) {
    }

    get backRequested() { return this.results.backRequested; }

    get resourceGroupSelected() { return this.results.resourceGroupSelected; }

    get modCategorySelected() { return this.results.modCategorySelected; }

    get configureInstallRequested() { return this.results.configureInstallRequested; }

    get installRequested() { return this.results.installRequested; }
}

export class AppDetailPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly appComponent: AppComponent;
    private readonly currentVersionComponent: CurrentVersionComponent;
    private readonly installConfigurationListCard: InstallConfigurationListCard;
    private readonly appOptionsAlert: IMessageAlert;
    private readonly appOptionsTextComponent: TextComponent;
    private readonly optionsAlert: IMessageAlert;
    private readonly optionsTextComponent: TextComponent;
    private readonly resourceGroupListCard: ResourceGroupListCard;
    private readonly modifierCategoryListCard: ModifierCategoryListCard;
    private readonly mostRecentRequestListCard: MostRecentRequestListCard;
    private readonly mostRecentErrorEventListCard: MostRecentErrorEventListCard;
    private readonly refreshPublishedVersionsCommand: AsyncCommand;
    private readonly installCurrentVersionCommand: AsyncCommand;
    private readonly backCommand = new Command(this.back.bind(this));
    private readonly installConfigurations: InstallConfiguration[] = [];
    private app = new App();
    private version = new XtiVersion();
    private permissions: IAppPermissions | null = null;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppDetailPanelView
    ) {
        this.alert = new MessageAlert(view.alertView);
        this.appComponent = new AppComponent(hubClient, view.app);
        this.currentVersionComponent = new CurrentVersionComponent(hubClient, view.currentVersion);
        this.installConfigurationListCard = new InstallConfigurationListCard(hubClient, view.installConfigurationListCardView);
        this.installConfigurationListCard.when.configureRequested.then(this.configureInstallRequested.bind(this));
        this.installConfigurationListCard.hide();
        this.appOptionsAlert = new CardAlert(view.appOptionsAlertView);
        this.appOptionsTextComponent = new TextComponent(view.appOptionsTextView);
        this.optionsAlert = new CardAlert(view.optionsAlertView);
        this.optionsTextComponent = new TextComponent(view.optionsTextView);
        this.resourceGroupListCard = new ResourceGroupListCard(hubClient, view.resourceGroupListCard);
        this.resourceGroupListCard.when.resourceGroupClicked.then(
            this.onResourceGroupSelected.bind(this)
        );
        this.modifierCategoryListCard = new ModifierCategoryListCard(hubClient, view.modifierCategoryListCard);
        this.modifierCategoryListCard.when.modCategorySelected.then(
            this.onModCategorySelected.bind(this)
        );
        this.mostRecentRequestListCard = new MostRecentRequestListCard(hubClient, view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard(hubClient, view.mostRecentErrorEventListCard);
        this.refreshPublishedVersionsCommand = new AsyncCommand(this.refreshPublishedVersions.bind(this));
        this.refreshPublishedVersionsCommand.add(view.refreshPublishedVersionsButton);
        this.refreshPublishedVersionsCommand.setText("Refresh Published Versions");
        this.refreshPublishedVersionsCommand.hide();
        this.installCurrentVersionCommand = new AsyncCommand(this.installCurrentVersion.bind(this));
        this.installCurrentVersionCommand.add(view.installCurrentVersionButton);
        this.installCurrentVersionCommand.setText("Install Current Version");
        this.installCurrentVersionCommand.hide();

        this.backCommand.add(view.backButton);
    }

    private configureInstallRequested(args: ConfigureInstallEventArgs) {
        const app = this.app;
        this.awaitable.resolve(Result.configureInstallRequested(
            app,
            this.installConfigurations,
            args.installConfiguration
        ));
    }

    private async refreshPublishedVersions() {
        await this.alert.infoAction(
            "Refreshing Published Versions...",
            () => this.hubClient.App.UpdateVersionsFromPublished()
        );
        await this.refresh();
    }

    private async installCurrentVersion() {
        const app = this.app;
        const version = this.version;
        this.awaitable.resolve(Result.installRequested(
            app,
            version,
            this.installConfigurations
        ));
    }

    private onResourceGroupSelected(group: AppResourceGroup) {
        this.awaitable.resolve(
            Result.resourceGroupSelected(group)
        );
    }

    private onModCategorySelected(modCategory: ModifierCategory) {
        this.awaitable.resolve(
            Result.modCategorySelected(modCategory)
        );
    }

    async refresh() {
        if (this.permissions?.modKey !== XtiUrl.current().path.modifier) {
            this.permissions = await this.alert.infoAction(
                "Loading...",
                () => new HubPermissions(this.hubClient).appPermissions()
            );
            if (this.permissions.canManageInstallation) {
                this.installConfigurationListCard.show();
            }
        }
        const promises: Promise<any>[] = [
            this.refreshApp(),
            this.refreshCurrentVersion(),
            this.refreshInstallConfigurations(),
            this.refreshDefaultAppOptions(),
            this.refreshDefaultOptions(),
            this.resourceGroupListCard.refresh(),
            this.modifierCategoryListCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        await Promise.all(promises);
        if (
            this.installConfigurations.length > 0 &&
            (!this.app.appKey.name.equals("Hub") || !this.app.appKey.isWebApp) &&
            (!this.app.appKey.name.equals("Support") || !this.app.appKey.isServiceApp)
        ) {
            this.installCurrentVersionCommand.show();
        }
    }

    private async refreshApp() {
        this.app = await this.appComponent.refresh();
        if (this.permissions.canManageInstallation) {
            this.refreshPublishedVersionsCommand.show();
        }
    }

    private async refreshCurrentVersion() {
        this.version = await this.currentVersionComponent.refresh();
    }

    private async refreshInstallConfigurations() {
        if (this.permissions.canManageInstallation) {
            this.installCurrentVersionCommand.hide();
            const installConfigurations = await this.installConfigurationListCard.refresh();
            this.installConfigurations.splice(0, this.installConfigurations.length, ...installConfigurations);
        }
    }

    private async refreshDefaultAppOptions() {
        this.appOptionsTextComponent.setText("");
        this.view.showAppOptions();
        const defaultAppOptions = await this.appOptionsAlert.infoAction(
            "Loading...",
            () => this.hubClient.App.GetDefaultAppOptions()
        );
        if (defaultAppOptions) {
            const stringified = JSON.stringify(JSON.parse(defaultAppOptions), undefined, 2);
            this.appOptionsTextComponent.setText(stringified);
        }
        else {
            this.view.hideAppOptions();
        }
    }

    private async refreshDefaultOptions() {
        this.optionsTextComponent.setText("");
        this.view.showAppOptions();
        const defaultOptions = await this.optionsAlert.infoAction(
            "Loading...",
            () => this.hubClient.App.GetDefaultOptions()
        );
        if (defaultOptions) {
            const stringified = JSON.stringify(JSON.parse(defaultOptions), undefined, 2);
            console.log(stringified);
            this.optionsTextComponent.setText(stringified);
        }
        else {
            this.view.hideOptions();
        }
    }

    private back() {
        this.awaitable.resolve(Result.backRequested());
    }

    start() {
        return this.awaitable.start();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}