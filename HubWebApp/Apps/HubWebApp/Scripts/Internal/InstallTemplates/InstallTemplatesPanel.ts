import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { InstallConfigurationTemplate } from "../../Lib/InstallConfigurationTemplate";
import { InstallTemplateListFactory, InstallTemplateListItem } from "./InstallTemplateListItem";
import { InstallTemplateListItemView } from "./InstallTemplateListItemView";
import { InstallTemplatesPanelView } from "./InstallTemplatesPanelView";

interface IResult {
    configureTemplateRequested?: { templates: InstallConfigurationTemplate[], template: InstallConfigurationTemplate };
    mainMenuRequested?: boolean;
}

class Result {
    static configureTemplateRequested(templates: InstallConfigurationTemplate[], template: InstallConfigurationTemplate) {
        return new Result({ configureTemplateRequested: { templates: templates, template: template } });
    }

    static mainMenuRequested() { return new Result({ mainMenuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get configureTemplateRequested() { return this.result.configureTemplateRequested; }

    get mainMenuRequested() { return this.result.mainMenuRequested; }
}

export class InstallTemplatesPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: IMessageAlert;
    private readonly templateListGroup: ListGroup<InstallTemplateListItem, InstallTemplateListItemView>;
    private readonly refreshCommand: AsyncCommand;
    private readonly addCommand: Command;
    private readonly installTemplates: InstallConfigurationTemplate[] = [];

    constructor(private readonly hubClient: HubAppClient, private readonly view: InstallTemplatesPanelView) {
        this.alert = new CardAlert(view.cardAlertView);
        this.templateListGroup = new ListGroup(view.templateListView, new InstallTemplateListFactory());
        this.templateListGroup.when.itemClicked.then(this.onTemplateClicked.bind(this));
        this.refreshCommand = new AsyncCommand(this._refresh.bind(this));
        this.refreshCommand.add(view.refreshButton);
        this.refreshCommand.animateIconWhenInProgress("spin");
        new Command(this.requestMainMenu.bind(this)).add(view.menuButton);
        this.addCommand = new Command(this.requestAdd.bind(this));
        this.addCommand.add(view.addButton);
        this.addCommand.hide();
    }

    private requestMainMenu() { this.awaitable.resolve(Result.mainMenuRequested()); }

    private requestAdd() {
        this.awaitable.resolve(Result.configureTemplateRequested(this.installTemplates, new InstallConfigurationTemplate()));
    }

    private onTemplateClicked(templateListItem: InstallTemplateListItem) {
        this.awaitable.resolve(Result.configureTemplateRequested(this.installTemplates, templateListItem.installTemplate));
    }

    refresh() { return this.refreshCommand.execute(); }

    private async _refresh() {
        this.addCommand.hide();
        const sourceTemplates = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.InstallTemplates.GetInstallTemplates()
        );
        const templates = sourceTemplates.map(t => new InstallConfigurationTemplate(t));
        this.installTemplates.splice(0, this.installTemplates.length, ...templates);
        this.templateListGroup.setItems(templates);
        if (templates.length === 0) {
            this.alert.danger("No Install Templates were found.");
        }
        this.addCommand.show();
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}