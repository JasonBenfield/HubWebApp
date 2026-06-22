import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { InstallConfigurationTemplate } from "../../../Lib/InstallConfigurationTemplate";
import { InstallTemplateListFactory, InstallTemplateListItem } from "../../InstallTemplates/InstallTemplateListItem";
import { InstallTemplateListItemView } from "../../InstallTemplates/InstallTemplateListItemView";
import { SelectInstallTemplatesPanelView } from "./SelectInstallTemplatesPanelView";

interface IResult {
    templateSelected?: { template: InstallConfigurationTemplate };
    back?: boolean;
}

class Result {
    static templateSelected(template: InstallConfigurationTemplate) {
        return new Result({ templateSelected: { template: template } });
    }

    static back() { return new Result({ back: true }); }

    private constructor(private readonly result: IResult) { }

    get templateSelected() { return this.result.templateSelected; }

    get back() { return this.result.back; }
}

export class SelectInstallTemplatesPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: IMessageAlert;
    private readonly templateListGroup: ListGroup<InstallTemplateListItem, InstallTemplateListItemView>;

    constructor(private readonly hubClient: HubAppClient, private readonly view: SelectInstallTemplatesPanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.templateListGroup = new ListGroup(view.templateListView, new InstallTemplateListFactory());
        this.templateListGroup.when.itemClicked.then(this.onTemplateClicked.bind(this));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    private onTemplateClicked(templateListItem: InstallTemplateListItem) {
        this.awaitable.resolve(Result.templateSelected(templateListItem.installTemplate));
    }

    async refresh() {
        const sourceTemplates = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.InstallTemplates.GetInstallTemplates()
        );
        const templates = sourceTemplates.map(t => new InstallConfigurationTemplate(t));
        this.templateListGroup.setItems(templates);
        if (templates.length === 0) {
            this.alert.danger("No Install Templates were found.");
        }
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}