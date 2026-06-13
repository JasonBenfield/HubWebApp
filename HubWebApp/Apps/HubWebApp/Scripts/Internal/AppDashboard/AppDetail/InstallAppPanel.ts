import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";
import { InstallConfigurationListItem } from "./InstallConfigurationListItem";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";
import { InstallAppPanelView } from "./InstallAppPanelView";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AppInstallCommandDetail } from "../../../Lib/AppInstallCommandDetail";
import { WebPage } from "@jasonbenfield/sharedwebapp/Http/WebPage";

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
    private readonly configurationListGroup: ListGroup<InstallConfigurationListItem, InstallConfigurationListItemView>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallAppPanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.configurationListGroup = new ListGroup(view.configurationListView);
        this.configurationListGroup.when.itemClicked.then(this.onConfigurationClicked.bind(this));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    private onConfigurationClicked(listItem: InstallConfigurationListItem) {
        return this.startInstall(listItem.configuration);
    }

    setInstallConfigurations(installConfigurations: InstallConfiguration[]) {
        this.configurationListGroup.setItems(
            installConfigurations,
            (c, itemView) => new InstallConfigurationListItem(c, itemView)
        );
    }

    start() {
        return this.awaitable.start();
    }

    private async startInstall(installConfiguration: InstallConfiguration) {
        const sourceCommandDetail = await this.alert.infoAction(
            "Installing...",
            () => this.hubClient.App.AddInstallCommand({
                ConfigurationID: installConfiguration.id
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