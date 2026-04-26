import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { AppDeleteCommandDetail } from "../../Lib/AppDeleteCommandDetail";
import { AppInstallCommandDetail } from "../../Lib/AppInstallCommandDetail";
import { IAppCommandDetail } from "../../Lib/IAppCommandDetail";
import { CommandCardView } from "./CommandCardView";

export class CommandCard extends BasicComponent {
    private readonly titleTextComponent: TextComponent;
    private readonly alert: IMessageAlert;
    private readonly appFormGroup: FormGroupText;
    private readonly locationFormGroup: FormGroupText;
    private readonly versionTextComponent: TextComponent;
    private readonly currentVersionTextComponent: TextComponent;
    private readonly timeStartedFormGroup: FormGroupText;

    constructor(protected readonly view: CommandCardView) {
        super(view);
        this.titleTextComponent = this.addComponent(new TextComponent(view.titleTextView));
        this.alert = this.addComponent(new CardAlert(view.cardAlertView));
        this.appFormGroup = this.addComponent(new FormGroupText(view.appFormGroupView));
        this.appFormGroup.setCaption("App");
        this.locationFormGroup = this.addComponent(new FormGroupText(view.locationFormGroupView));
        this.locationFormGroup.setCaption("Location");
        this.versionTextComponent = this.addComponent(new TextComponent(view.versionTextView));
        this.currentVersionTextComponent = this.addComponent(new TextComponent(view.currentVersionTextView));
        this.timeStartedFormGroup = this.addComponent(new FormGroupText(view.timeStartedFormGroupView));
    }

    setCommandDetail(commandDetail: IAppCommandDetail) {
        const machineName = commandDetail.location.qualifiedMachineName || "localhost";
        this.titleTextComponent.setText(`${commandDetail.command.commandName.value} Command [ ${machineName} ]`);
        if (commandDetail.command.isPending) {
            this.alert.warning("Not Started");
        }
        else if (commandDetail.command.isInProgress) {
            this.alert.info(`Started at ${commandDetail.command.timeStarted.format()}`);
        }
        else if (commandDetail.command.isComplete) {
            this.alert.success(`Completed at ${commandDetail.command.timeEnded.format()}`);
        }
        else if (commandDetail.command.isFailed) {
            this.alert.danger(`Failed at ${commandDetail.command.timeEnded.format()}`);
        }
        else {
            this.alert.clear();
        }
        this.appFormGroup.setValue(commandDetail.app.appKey.format());
        this.locationFormGroup.setValue(commandDetail.location.qualifiedMachineName || "localhost");
        if (commandDetail.command.isComplete || commandDetail.command.isFailed) {
            this.timeStartedFormGroup.setValue(commandDetail.command.timeStarted.format());
            this.timeStartedFormGroup.show();
        }
        else {
            this.timeStartedFormGroup.hide();
        }
        this.view.versionFormGroupView.hide();
        if (commandDetail instanceof AppInstallCommandDetail) {
            this.versionTextComponent.setText(commandDetail.version.versionKey.displayText);
            this.currentVersionTextComponent.setText("");
            this.view.versionFormGroupView.show();
        }
        else if (commandDetail instanceof AppDeleteCommandDetail) {
            this.versionTextComponent.setText(commandDetail.version.versionKey.displayText);
            this.currentVersionTextComponent.setText(commandDetail.installation.isCurrent ? "[ Current ]" : "");
            this.view.versionFormGroupView.show();
        }
    }

    show() { this.view.show(); }

    hide() { this.view.hide(); }
}