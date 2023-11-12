import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";
import { ModCategoryListItem } from "./ModCategoryListItem";
import { SelectModCategoryPanelView } from "./SelectModCategoryPanelView";

interface IResult {
    back?: boolean;
    defaultModSelected?: boolean;
    modCategorySelected?: { modCategory: IModifierCategoryModel; };
}

class Result {
    static back() { return new Result({ back: true }); }

    static defaultModSelected() {
        return new Result({ defaultModSelected: true });
    }

    static modCategorySelected(modCategory: IModifierCategoryModel) {
        return new Result({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: IResult) { }

    get back() { return this.results.back; }

    get defaultModSelected() {
        return this.results.defaultModSelected;
    }

    get modCategorySelected() {
        return this.results.modCategorySelected;
    }
}

export class SelectModCategoryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly modCategories: ListGroup<ModCategoryListItem, ModCategoryButtonListItemView>;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: SelectModCategoryPanelView
    ) {
        new TextComponent(this.view.titleHeader).setText('Modifier Categories');
        this.alert = new CardAlert(view.alert).alert;
        this.modCategories = new ListGroup(view.modCategories);
        this.modCategories.when.itemClicked.then(this.onModCategoryClicked.bind(this));
        new Command(this.onDefaultModifierClicked.bind(this)).add(view.defaultModButton);
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private onDefaultModifierClicked() {
        this.awaitable.resolve(Result.defaultModSelected());
    }

    private back() {
        this.awaitable.resolve(Result.back());
    }

    private onModCategoryClicked(item: ModCategoryListItem) {
        this.awaitable.resolve(
            Result.modCategorySelected(item.modCategory)
        );
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let modCategories = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.App.GetModifierCategories()
        );
        modCategories = modCategories.filter(mc => mc.Name.Value.toLowerCase() !== 'default');
        if (modCategories.length === 0) {
            this.awaitable.resolve(Result.defaultModSelected());
        }
        else {
            this.modCategories.setItems(
                modCategories,
                (mc, itemView) => new ModCategoryListItem(mc, itemView)
            );
        }
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}