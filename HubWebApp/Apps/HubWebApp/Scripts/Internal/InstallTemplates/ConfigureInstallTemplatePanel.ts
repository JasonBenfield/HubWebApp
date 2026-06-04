import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { ConfigureInstallTemplatePanelView } from "./ConfigureInstallTemplatePanelView";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { FormGroupInput } from "@jasonbenfield/sharedwebapp/Forms/FormGroupInput";
import { FormGroupTextInput } from "@jasonbenfield/sharedwebapp/Forms/FormGroupTextInput";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { InstallConfigurationTemplate } from "../../Lib/InstallConfigurationTemplate";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { HubAppClient } from "../../Lib/Http/HubAppClient";

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

export class ConfigureInstallTemplatePanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: IMessageAlert;
    private readonly templateNameFormGroup: FormGroupText;
    private readonly templateNameInputFormGroup: FormGroupTextInput;
    private readonly machineNameInputFormGroup: FormGroupTextInput;
    private readonly domainInputFormGroup: FormGroupTextInput;
    private readonly siteNameInputFormGroup: FormGroupTextInput;
    private readonly saveCommand: AsyncCommand;
    private installTemplate = new InstallConfigurationTemplate();

    constructor(private readonly hubClient: HubAppClient, private readonly view: ConfigureInstallTemplatePanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.templateNameFormGroup = new FormGroupText(view.templateNameFormGroupView);
        this.templateNameFormGroup.setCaption("Template Name");
        this.templateNameInputFormGroup = new FormGroupTextInput(view.templateNameInputFormGroupView);
        this.templateNameInputFormGroup.setCaption("Template Name");
        this.machineNameInputFormGroup = new FormGroupTextInput(view.machineNameInputFormGroupView);
        this.machineNameInputFormGroup.setCaption("Machine Name");
        this.domainInputFormGroup = new FormGroupTextInput(view.domainInputFormGroupView);
        this.domainInputFormGroup.setCaption("Domain");
        this.siteNameInputFormGroup = new FormGroupTextInput(view.siteNameInputFormGroupView);
        this.siteNameInputFormGroup.setCaption("Site Name");
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
    }

    private cancel() { this.awaitable.resolve(Result.cancelled()); }

    private async save() {
        const templateName = this.installTemplate.isFound ?
            this.installTemplate.templateName :
            this.templateNameInputFormGroup.getValue()?.trim() || "";
        const machineName = this.machineNameInputFormGroup.getValue()?.trim() || "";
        const domain = this.domainInputFormGroup.getValue()?.trim() || "";
        const siteName = this.siteNameInputFormGroup.getValue()?.trim() || "";
        await this.alert.infoAction(
            "Saving...",
            () => this.hubClient.Install.ConfigureInstallTemplate({
                TemplateName: templateName,
                DestinationMachineName: machineName,
                Domain: domain,
                SiteName: siteName
            })
        );
        this.awaitable.resolve(Result.saved());
    }

    setTemplate(installTemplate: InstallConfigurationTemplate) {
        this.installTemplate = installTemplate;
        this.templateNameFormGroup.setValue(installTemplate.templateName);
        if (installTemplate.isFound) {
            this.templateNameFormGroup.show();
        }
        else {
            this.templateNameFormGroup.hide();
        }
        this.templateNameInputFormGroup.setValue(installTemplate.templateName);
        if (installTemplate.isFound) {
            this.templateNameInputFormGroup.hide();
        }
        else {
            this.templateNameInputFormGroup.show();
        }
        this.machineNameInputFormGroup.setValue(installTemplate.destinationMachineName);
        this.domainInputFormGroup.setValue(installTemplate.domain);
        this.siteNameInputFormGroup.setValue(installTemplate.siteName);
    }

    start() {
        return this.awaitable.start();
    }

    activate() {
        this.view.show();
        if (this.installTemplate.isFound) {
            this.machineNameInputFormGroup.setFocus();
        }
        else {
            this.templateNameInputFormGroup.setFocus();
        }
    }

    deactivate() { this.view.hide(); }

}