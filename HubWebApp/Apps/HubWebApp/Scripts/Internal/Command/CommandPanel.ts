import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/Common";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { AppCommand } from "../../Lib/AppCommand";
import { AppDeleteCommandDetail } from "../../Lib/AppDeleteCommandDetail";
import { AppInstallCommandDetail } from "../../Lib/AppInstallCommandDetail";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { IAppCommandDetail } from "../../Lib/IAppCommandDetail";
import { CommandCard } from "./CommandCard";
import { CommandPanelView } from "./CommandPanelView";
import { CommandStepListCard } from "./CommandStepListCard";

interface IResult {
    menu?: boolean;
}

class Result {
    static menu() {
        return new Result({ menu: true });
    }

    private constructor(private readonly result: IResult) { }

    get menu() { return this.result.menu; }
}

export class CommandPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly commandCard: CommandCard;
    private readonly viewAppLinkComponent: TextLinkComponent;
    private readonly viewCurrentInstallationLinkComponent: TextLinkComponent;
    private readonly viewVersionInstallationLinkComponent: TextLinkComponent;
    private readonly viewInstallationLinkComponent: TextLinkComponent;
    private readonly stepListCard: CommandStepListCard;
    private readonly refreshCommand: AsyncCommand;
    private timeoutTime = DateTimeOffset.max();
    private timeoutID: number | null = null;
    private commandID = 0;
    private command = new AppCommand();

    constructor(private readonly hubClient: HubAppClient, private readonly view: CommandPanelView) {
        this.alert = new MessageAlert(view.alertView);
        this.commandCard = new CommandCard(view.commandCardView);
        this.commandCard.hide();
        this.viewAppLinkComponent = new TextLinkComponent(view.viewAppButton);
        this.viewAppLinkComponent.setText("View App");
        this.viewCurrentInstallationLinkComponent = new TextLinkComponent(view.viewCurrentInstallationButton);
        this.viewCurrentInstallationLinkComponent.setText("View Current Installation");
        this.viewVersionInstallationLinkComponent = new TextLinkComponent(view.viewVersionInstallationButton);
        this.viewVersionInstallationLinkComponent.setText("View Version Installation");
        this.viewInstallationLinkComponent = new TextLinkComponent(view.viewInstallationButton);
        this.viewInstallationLinkComponent.setText("View Installation");
        this.stepListCard = new CommandStepListCard(view.stepListCardView);
        this.stepListCard.hide();
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.refreshCommand = new AsyncCommand(this._refresh.bind(this));
        this.refreshCommand.add(view.refreshButton);
        this.refreshCommand.animateIconWhenInProgress("spin");
    }

    private menu() { this.awaitable.resolve(Result.menu()); }

    setCommandID(commandID: number) {
        this.commandID = commandID;
        this.command = new AppCommand();
        this.timeoutID = null;
        this.stepListCard.clear();
    }

    start() {
        return this.awaitable.start();
    }

    async refresh() {
        this.viewAppLinkComponent.hide();
        this.viewCurrentInstallationLinkComponent.hide();
        this.viewVersionInstallationLinkComponent.hide();
        this.viewInstallationLinkComponent.hide();
        this.command = await this.getCommand();
        await this.refreshCommand.execute();
        this.timeoutTime = DateTimeOffset.now().addMinutes(5);
        this.autoRefresh();
    }

    private autoRefresh() {
        const command = this.command;
        if ((command.isPending || command.isInProgress) && DateTimeOffset.now().isBefore(this.timeoutTime)) {
            this.timeoutID = window.setTimeout(
                async () => {
                    await this.refreshCommand.execute();
                    this.autoRefresh();
                },
                5000
            );
        }
        else {
            this.timeoutID = null;
        }
    }

    private async _refresh() {
        const command = this.command;
        if (command.isFound) {
            const commandDetail = await this.getCommandDetail(command);
            this.command = commandDetail.command;
            this.commandCard.setCommandDetail(commandDetail);
            this.commandCard.show();
            this.stepListCard.addOrUpdateCommandSteps(commandDetail.steps);
            if (commandDetail.steps.length > 0) {
                this.stepListCard.show();
            }
            else {
                this.stepListCard.hide();
            }
            this.viewAppLinkComponent.setHref(
                this.hubClient.App.Index.getUrl({})
            );
            if (commandDetail instanceof AppInstallCommandDetail) {
                const currentInstallation = commandDetail.getCurrentInstallationOrDefault();
                if (currentInstallation.isFound) {
                    this.viewCurrentInstallationLinkComponent.setHref(
                        this.hubClient.Installation.Index.getUrl({ InstallationID: currentInstallation.id })
                    );
                    this.viewCurrentInstallationLinkComponent.show();
                }
                const versionInstallation = commandDetail.getVersionInstallationOrDefault();
                if (versionInstallation.isFound) {
                    this.viewVersionInstallationLinkComponent.setHref(
                        this.hubClient.Installation.Index.getUrl({ InstallationID: versionInstallation.id })
                    );
                    this.viewVersionInstallationLinkComponent.show();
                }
            }
            else if (commandDetail instanceof AppDeleteCommandDetail) {
                this.viewInstallationLinkComponent.setHref(
                    this.hubClient.Installation.Index.getUrl({ InstallationID: commandDetail.installation.id })
                );
                this.viewInstallationLinkComponent.show();
            }
        }
    }

    private async getCommand() {
        const sourceCommand = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.Command.GetCommand({ CommandID: this.commandID })
        );
        return new AppCommand(sourceCommand);
    }

    private async getCommandDetail(command: AppCommand) {
        let commandDetail: IAppCommandDetail;
        if (command.commandName.isInstall) {
            const sourceInstallCommandDetail = await this.alert.infoAction(
                "Loading...",
                () => this.hubClient.Command.GetInstallationCommandDetail({
                    CommandID: this.commandID
                })
            );
            commandDetail = new AppInstallCommandDetail(sourceInstallCommandDetail);
        }
        else if (command.commandName.isDelete) {
            const sourceDeleteCommandDetail = await this.alert.infoAction(
                "Loading...",
                () => this.hubClient.Command.GetDeleteCommandDetail({
                    CommandID: this.commandID
                })
            );
            commandDetail = new AppDeleteCommandDetail(sourceDeleteCommandDetail);
        }
        else {
            throw Error(`Command '${command.commandName.value}' is not supported.`);
        }
        return commandDetail;
    }

    activate() { this.view.show(); }

    deactivate() {
        this.view.hide();
        this.command = new AppCommand();
        const timeoutID = this.timeoutID;
        if (timeoutID) {
            window.clearTimeout(timeoutID);
        }
        this.timeoutID = null;
    }

}