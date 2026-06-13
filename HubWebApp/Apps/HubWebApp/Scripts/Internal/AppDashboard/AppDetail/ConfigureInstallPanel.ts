import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { ConfigureInstallPanelView } from "./ConfigureInstallPanelView";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { FormGroupInput } from "@jasonbenfield/sharedwebapp/Forms/FormGroupInput";
import { FormGroupTextInput } from "@jasonbenfield/sharedwebapp/Forms/FormGroupTextInput";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ModalError } from "@jasonbenfield/sharedwebapp/Components/ModalError";
import { ErrorModel } from "@jasonbenfield/sharedwebapp/ErrorModel";
import { TextToNumberViewValue } from "@jasonbenfield/sharedwebapp/Forms/TextToNumberViewValue";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";
import { App } from "../../../Lib/App";
import { AppType } from "../../../Lib/Http/AppType";
import { InstallConfigurationTemplate } from "../../../Lib/InstallConfigurationTemplate";
import { ModalConfirm } from "@jasonbenfield/sharedwebapp/Components/ModalConfirm";

interface IResult {
    saved?: boolean;
    cancelled?: boolean;
}

class Result {
    static saved() {
        return new Result({ saved: true });
    }

    static cancelled() { return new Result({ cancelled: true }); }

    private constructor(private readonly result: IResult) { }

    get saved() { return this.result.saved; }

    get cancelled() { return this.result.cancelled; }
}

export class ConfigureInstallPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: IMessageAlert;
    private readonly configurationNameFormGroup: FormGroupText;
    private readonly configurationNameInputFormGroup: FormGroupTextInput;
    private readonly templateNameFormGroup: FormGroupText;
    private readonly machineNameFormGroup: FormGroupText;
    private readonly installSequenceInputFormGroup: FormGroupInput<number>;
    private readonly saveCommand: AsyncCommand;
    private readonly modalConfirm: ModalConfirm;
    private readonly modalError: ModalError;
    private readonly installConfigurations: InstallConfiguration[] = [];
    private installTemplate = new InstallConfigurationTemplate();
    private installConfiguration = new InstallConfiguration();

    constructor(private readonly hubClient: HubAppClient, private readonly view: ConfigureInstallPanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.configurationNameFormGroup = new FormGroupText(view.configurationNameFormGroupView);
        this.configurationNameFormGroup.setCaption("Configuration Name");
        this.configurationNameInputFormGroup = new FormGroupTextInput(view.configurationNameInputFormGroupView);
        this.configurationNameInputFormGroup.setCaption("Configuration Name");
        this.templateNameFormGroup = new FormGroupText(view.templateNameFormGroupView);
        this.templateNameFormGroup.setCaption("Template Name");
        this.machineNameFormGroup = new FormGroupText(view.machineNameFormGroupView);
        this.machineNameFormGroup.setCaption("Machine Name");
        this.installSequenceInputFormGroup = new FormGroupInput(
            view.installSequenceInputFormGroupView,
            new TextToNumberViewValue()
        );
        this.installSequenceInputFormGroup.setCaption("Install Sequence");
        this.modalConfirm = new ModalConfirm(view.modalConfirmView);
        this.modalError = new ModalError(view.modalErrorView);
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
        new AsyncCommand(this.deleteInstallConfiguration.bind(this)).add(view.deleteButton);
    }

    private cancel() { this.awaitable.resolve(Result.cancelled()); }

    private async save() {
        const errors = this.validate();
        if (errors.length > 0) {
            this.modalError.show(errors.map(e => new ErrorModel(e)));
        }
        else {
            const configurationName = this.installConfiguration.isFound ?
                this.installConfiguration.configurationName :
                this.configurationNameInputFormGroup.getValue()?.trim() || "";
            const installSequence = this.installSequenceInputFormGroup.getValue();
            await this.alert.infoAction(
                "Saving...",
                () => this.hubClient.App.ConfigureInstall({
                    ConfigurationName: configurationName,
                    TemplateID: this.installTemplate.id,
                    InstallSequence: installSequence
                })
            );
            this.awaitable.resolve(Result.saved());
        }
    }

    private async deleteInstallConfiguration() {
        const isConfirmed = await this.modalConfirm.confirm("Confirm Delete", "Delete this install configuration?");
        if (isConfirmed) {
            await this.alert.infoAction(
                "Deleting...",
                () => this.hubClient.App.DeleteInstallConfiguration({
                    ConfigurationID: this.installConfiguration.id
                })
            );
            this.awaitable.resolve(Result.saved());
        }
    }

    private validate() {
        const errors: string[] = [];
        if (!this.installConfiguration.isFound) {
            const configurationName = this.configurationNameInputFormGroup.getValue()?.trim().toUpperCase() || "";
            if (configurationName && this.installConfigurations.find(t => t.configurationName.toUpperCase() === configurationName)) {
                errors.push(`Install Configuration '${configurationName}' already exists.`);
            }
        }
        return errors;
    }

    setInstallTemplate(installTemplate: InstallConfigurationTemplate) {
        this.installTemplate = installTemplate;
        this.templateNameFormGroup.setValue(installTemplate.templateName);
        this.machineNameFormGroup.setValue(installTemplate.destinationMachineName);
    }

    setInstallConfiguration(app: App, installConfigurations: InstallConfiguration[], installConfiguration: InstallConfiguration) {
        this.installConfigurations.splice(0, this.installConfigurations.length, ...installConfigurations);
        this.installConfiguration = installConfiguration;
        this.configurationNameFormGroup.setValue(installConfiguration.configurationName);
        this.configurationNameInputFormGroup.setValue(installConfiguration.configurationName);
        if (installConfiguration.isFound) {
            this.configurationNameFormGroup.show();
            this.configurationNameInputFormGroup.hide();
            this.installSequenceInputFormGroup.setValue(installConfiguration.installSequence);
        }
        else {
            this.configurationNameFormGroup.hide();
            this.configurationNameInputFormGroup.show();
            this.installSequenceInputFormGroup.setValue(
                app.appKey.type.equals(AppType.values.WebApp) ? 1 : 100
            )
        }
    }

    start() {
        return this.awaitable.start();
    }

    activate() {
        this.view.show();
        if (this.installConfiguration.isFound) {
            this.installSequenceInputFormGroup.setFocus();
        }
        else {
            this.configurationNameInputFormGroup.setFocus();
        }
    }

    deactivate() { this.view.hide(); }

}