import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { WebPage } from "@jasonbenfield/sharedwebapp/Http/WebPage";
import { AppInstallCommandDetail } from "../../../Lib/AppInstallCommandDetail";
import { AppKey } from "../../../Lib/AppKey";
import { AppVersionKey } from "../../../Lib/AppVersionKey";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";
import { InstallAppPanelView } from "./InstallAppPanelView";
import { InstallConfigurationListItem } from "./InstallConfigurationListItem";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";

interface IResult {
    back?: boolean;
}

class Result {
    static back() {
        return new Result({ back: true });
    }

    private constructor(private readonly result: IResult) { }

    get back() { return this.result.back; }
}

export class InstallAppPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: IMessageAlert;
    private readonly appKeyFormGroup: FormGroupText;
    private readonly versionKeyFormGroup: FormGroupText;
    private readonly configurationListGroup: ListGroup<InstallConfigurationListItem, InstallConfigurationListItemView>;
    private versionKey = new AppVersionKey();

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallAppPanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.appKeyFormGroup = new FormGroupText(view.appKeyFormGroupView);
        this.versionKeyFormGroup = new FormGroupText(view.versionKeyFormGroupView);
        this.configurationListGroup = new ListGroup(view.configurationListView);
        this.configurationListGroup.when.itemClicked.then(this.onConfigurationClicked.bind(this));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    private onConfigurationClicked(listItem: InstallConfigurationListItem) {
        return this.startInstall(listItem.configuration);
    }

    setApp(appKey: AppKey, versionKey: AppVersionKey) {
        this.versionKey = versionKey;
        this.appKeyFormGroup.setValue(appKey.format());
        this.versionKeyFormGroup.setValue(versionKey.displayText);
    }

    setInstallConfigurations(installConfigurations: InstallConfiguration[]) {
        this.configurationListGroup.setItems(
            installConfigurations,
            (c, itemView) => new InstallConfigurationListItem(c, itemView)
        );
        if (installConfigurations.length > 0) {
            this.alert.info("Select configuration to begin install.");
        }
        else {
            this.alert.danger("No install configurations were found.");
        }
    }

    start() {
        return this.awaitable.start();
    }

    private async startInstall(installConfiguration: InstallConfiguration) {
        const sourceCommandDetail = await this.alert.infoAction(
            "Installing...",
            () => this.hubClient.App.AddInstallCommand({
                VersionKey: this.versionKey.displayText,
                InstallConfigurationID: installConfiguration.id,
                InstallAsCurrent: true,
                IsAutoStartEnabled: false
            })
        );
        const commandDetail = new AppInstallCommandDetail(sourceCommandDetail);
        new WebPage(this.hubClient.Command.Index.getUrl({ CommandID: commandDetail.command.id }));
    }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }

}