import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";
import { ModifierListItem } from "./ModifierListItem";
import { SelectModifierPanelView } from "./SelectModifierPanelView";
import { Modifier } from "../../Lib/Modifier";
import { ModifierCategory } from "../../Lib/ModifierCategory";

interface IResult {
    back?: boolean;
    modifierSelected?: { modifier: Modifier; };
}

class Result {
    static back() { return new Result({ back: true }); }

    static modifierSelected(modifier: Modifier) {
        return new Result({
            modifierSelected: { modifier: modifier }
        });
    }

    private constructor(private readonly results: IResult) { }

    get back() { return this.results.back; }

    get modifierSelected() {
        return this.results.modifierSelected;
    }
}

export class SelectModifierPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly modifiers: ListGroup<ModifierListItem, ModifierButtonListItemView>;
    private modCategory: ModifierCategory;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: SelectModifierPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.modifiers = new ListGroup(this.view.modifiers);
        this.modifiers.when.itemClicked.then(this.onModifierClicked.bind(this));
        new Command(this.back.bind(this)).add(this.view.backButton);
    }

    private back() {
        this.awaitable.resolve(Result.back());
    }

    setModCategory(modCategory: ModifierCategory) {
        this.modCategory = modCategory;
    }

    private onModifierClicked(item: ModifierListItem) {
        this.awaitable.resolve(
            Result.modifierSelected(item.modifier)
        );
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        const sourceModifiers = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.ModCategory.GetModifiers(this.modCategory.id)
        );
        const modifiers = sourceModifiers.map(m => new Modifier(m));
        this.modifiers.setItems(
            modifiers,
            (mod, itemView) => new ModifierListItem(mod, itemView)
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}