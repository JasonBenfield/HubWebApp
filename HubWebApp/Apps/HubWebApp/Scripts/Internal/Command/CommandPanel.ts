import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { AppCommand } from "../../Lib/AppCommand";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { CommandPanelView } from "./CommandPanelView";
import { IAppCommandDetail } from "../../Lib/IAppCommandDetail";
import { AppInstallCommandDetail } from "../../Lib/AppInstallCommandDetail";
import { AppDeleteCommandDetail } from "../../Lib/AppDeleteCommandDetail";
import { CommandCard } from "./CommandCard";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";

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
    private readonly awaitable: Awaitable<Result>;
    private readonly alert: MessageAlert;
    private readonly commandCard: CommandCard;
    private commandID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: CommandPanelView) {
        this.alert = new MessageAlert(view.alertView);
        this.commandCard = new CommandCard(view.commandCardView);
        this.commandCard.hide();
        new Command(this.menu.bind(this)).add(view.menuButton);
    }

    private menu() { this.awaitable.resolve(Result.menu()); }

    setCommandID(commandID: number) {
        this.commandID = commandID;
    }

    start() {
        return this.awaitable.start();
    }

    async refresh() {
        const command = await this.getCommand();
        const commandDetail = await this.getCommandDetail(command);
        this.commandCard.setCommandDetail(commandDetail);
        this.commandCard.show();
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

    deactivate() { this.view.hide(); }

}