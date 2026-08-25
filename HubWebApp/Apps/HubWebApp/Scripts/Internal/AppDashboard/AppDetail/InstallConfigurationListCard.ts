import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { InstallConfigurationListCardView } from "./InstallConfigurationListCardView";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { InstallConfigurationListItem } from "./InstallConfigurationListItem";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";
import { EventSource } from "@jasonbenfield/sharedwebapp/Events";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";

export interface ConfigureInstallEventArgs {
    installConfiguration: InstallConfiguration
}

type Events = { configureRequested: ConfigureInstallEventArgs };

export class InstallConfigurationListCard extends BasicComponent {
    private readonly alert: IMessageAlert;
    private readonly configurationListGroup: ListGroup<InstallConfigurationListItem, InstallConfigurationListItemView>;
    private readonly installConfigurations: InstallConfiguration[] = [];
    private readonly eventSource = new EventSource<Events>(this, { configureRequested: null });
    readonly when = this.eventSource.when;

    constructor(private readonly hubClient: HubAppClient, view: InstallConfigurationListCardView) {
        super(view);
        this.alert = new CardAlert(view.cardAlertView);
        this.configurationListGroup = new ListGroup(view.configurationListView);
        new Command(this.add.bind(this)).add(view.addButton);
    }

    private add() {
        this.eventSource.events.configureRequested.invoke({
            installConfiguration: new InstallConfiguration()
        });
    }

    async refresh() {
        this.installConfigurations.splice(0, this.installConfigurations.length);
        const sourceConfigurations = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.App.GetInstallConfigurations()
        );
        const configurations = sourceConfigurations.map(c => new InstallConfiguration(c));
        this.installConfigurations.splice(0, this.installConfigurations.length, ...configurations);
        this.configurationListGroup.setItems(
            configurations,
            (c, itemView) => new InstallConfigurationListItem(c, itemView)
        );
        if (configurations.length === 0) {
            this.alert.warning("No configurations have been added.");
        }
        return configurations;
    }

    show() { this.view.show(); }

    hide() { this.view.hide(); }
}