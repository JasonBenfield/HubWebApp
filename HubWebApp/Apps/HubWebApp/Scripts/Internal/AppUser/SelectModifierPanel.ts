import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { ModifierButtonListItemView } from "./ModifierButtonListItemView";
import { ModifierListItem } from "./ModifierListItem";
import { SelectModifierPanelView } from "./SelectModifierPanelView";

interface IResult {
    back?: boolean;
    modifierSelected?: { modifier: IModifierModel; };
}

class Result {
    static back() { return new Result({ back: true }); }

    static modifierSelected(modifier: IModifierModel) {
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
    private modCategory: IModifierCategoryModel;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: SelectModifierPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.modifiers = new ListGroup(this.view.modifiers);
        this.modifiers.registerItemClicked(this.onModifierClicked.bind(this));
        new Command(this.back.bind(this)).add(this.view.backButton);
    }

    private back() {
        this.awaitable.resolve(Result.back());
    }

    setModCategory(modCategory: IModifierCategoryModel) {
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
        const modifiers = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.ModCategory.GetModifiers(this.modCategory.ID)
        );
        this.modifiers.setItems(
            modifiers,
            (mod, itemView) => new ModifierListItem(mod, itemView)
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}