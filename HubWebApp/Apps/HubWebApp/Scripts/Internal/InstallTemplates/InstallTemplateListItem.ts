import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { IListGroupFactory } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { InstallConfigurationTemplate } from "../../Lib/InstallConfigurationTemplate";
import { InstallTemplateListItemView } from "./InstallTemplateListItemView";
import { BasicListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";

export class InstallTemplateListFactory implements IListGroupFactory<InstallTemplateListItem, InstallTemplateListItemView> {
    createItem(sourceItem: InstallConfigurationTemplate, itemView: InstallTemplateListItemView) {
        return new InstallTemplateListItem(sourceItem, itemView);
    }

    createHeader(itemView: BasicListGroupItemView) {
        return new InstallTemplateListHeader(itemView as InstallTemplateListItemView);
    }

}

export class InstallTemplateListHeader extends BasicComponent {
    constructor(view: InstallTemplateListItemView) {
        super(view);
        const templateNameTextComponent = this.addComponent(new TextComponent(view.templateNameTextView))
        const machineNameTextComponent = this.addComponent(new TextComponent(view.machineNameTextView))
        const domainTextCompnent = this.addComponent(new TextComponent(view.domainTextView))
        const siteNameTextComponent = this.addComponent(new TextComponent(view.siteNameTextView))
        templateNameTextComponent.setText("Name");
        machineNameTextComponent.setText("Location");
        domainTextCompnent.setText("Domain");
        siteNameTextComponent.setText("Site Name");
        view.styleAsHeader();
    }
}

export class InstallTemplateListItem extends BasicComponent {
    constructor(
        readonly installTemplate: InstallConfigurationTemplate,
        view: InstallTemplateListItemView
    ) {
        super(view);
        const templateNameTextComponent = this.addComponent(new TextComponent(view.templateNameTextView))
        const machineNameTextComponent = this.addComponent(new TextComponent(view.machineNameTextView))
        const domainTextCompnent = this.addComponent(new TextComponent(view.domainTextView))
        const siteNameTextComponent = this.addComponent(new TextComponent(view.siteNameTextView))
        templateNameTextComponent.setText(installTemplate.templateName);
        machineNameTextComponent.setText(installTemplate.destinationMachineName);
        domainTextCompnent.setText(installTemplate.domain);
        siteNameTextComponent.setText(installTemplate.siteName);
    }
}