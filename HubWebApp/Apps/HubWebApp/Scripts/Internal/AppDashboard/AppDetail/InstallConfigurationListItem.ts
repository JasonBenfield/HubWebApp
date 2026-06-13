import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { InstallConfigurationListItemView } from "./InstallConfigurationListItemView";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { InstallConfiguration } from "../../../Lib/InstallConfiguration";

export class InstallConfigurationListItem extends BasicComponent {
    constructor(readonly configuration: InstallConfiguration, view: InstallConfigurationListItemView) {
        super(view);
        const templateNameTextComponent = this.addComponent(new TextComponent(view.templateNameTextView));
        const machineNameTextComponent = this.addComponent(new TextComponent(view.machineNameTextView));
        const configurationNameTextComponent = this.addComponent(new TextComponent(view.configurationNameTextView));
        templateNameTextComponent.setText(configuration.template.templateName);
        machineNameTextComponent.setText(configuration.template.destinationMachineName);
        configurationNameTextComponent.setText(configuration.configurationName);
    }
}