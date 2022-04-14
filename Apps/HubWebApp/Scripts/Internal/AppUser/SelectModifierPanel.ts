import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";
import { ModifierListItem } from "./ModifierListItem";
import { SelectModifierPanelView } from "./SelectModifierPanelView";

interface Results {
    back?: {};
    modifierSelected?: { modifier: IModifierModel; };
}

export class SelectModifierPanelResult {
    static back() { return new SelectModifierPanelResult({ back: {} }); }

    static modifierSelected(modifier: IModifierModel) {
        return new SelectModifierPanelResult({
            modifierSelected: { modifier: modifier }
        });
    }

    private constructor(private readonly results: Results) { }

    get back() { return this.results.back; }

    get modifierSelected() {
        return this.results.modifierSelected;
    }
}

export class SelectModifierPanel implements IPanel {
    private readonly awaitable = new Awaitable<SelectModifierPanelResult>();
    private readonly alert: MessageAlert;
    private readonly modifiers: ListGroup;
    private modCategory: IModifierCategoryModel;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: SelectModifierPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.modifiers = new ListGroup(this.view.modifiers);
        this.modifiers.itemClicked.register(this.onModifierClicked.bind(this));
        new Command(this.back.bind(this)).add(this.view.backButton);
    }

    private back() {
        this.awaitable.resolve(SelectModifierPanelResult.back());
    }

    setModCategory(modCategory: IModifierCategoryModel) {
        this.modCategory = modCategory;
    }

    private onModifierClicked(item: ModifierListItem) {
        this.awaitable.resolve(
            SelectModifierPanelResult.modifierSelected(item.modifier)
        );
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let modifiers = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.ModCategory.GetModifiers(this.modCategory.ID)
        );
        this.modifiers.setItems(
            modifiers,
            (mod: IModifierModel, itemView: ModifierButtonListItemView) =>
                new ModifierListItem(mod, itemView)
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}