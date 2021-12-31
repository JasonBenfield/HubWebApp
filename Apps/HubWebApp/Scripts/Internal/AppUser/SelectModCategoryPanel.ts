import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { FilteredArray } from "@jasonbenfield/sharedwebapp/Enumerable";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { ModCategoryButtonListItemView } from "./ModCategoryButtonListItemView";
import { ModCategoryListItem } from "./ModCategoryListItem";
import { SelectModCategoryPanelView } from "./SelectModCategoryPanelView";

interface Results {
    back?: {};
    defaultModSelected?: {};
    modCategorySelected?: { modCategory: IModifierCategoryModel; };
}

export class SelectModCategoryPanelResult {
    static back() { return new SelectModCategoryPanelResult({ back: {} }); }

    static defaultModSelected() {
        return new SelectModCategoryPanelResult({ defaultModSelected: {} });
    }

    static modCategorySelected(modCategory: IModifierCategoryModel) {
        return new SelectModCategoryPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    }

    private constructor(private readonly results: Results) { }

    get back() { return this.results.back; }

    get defaultModSelected() {
        return this.results.defaultModSelected;
    }

    get modCategorySelected() {
        return this.results.modCategorySelected;
    }
}

export class SelectModCategoryPanel implements IPanel {
    private readonly awaitable = new Awaitable<SelectModCategoryPanelResult>();
    private readonly alert: MessageAlert;
    private readonly modCategories: ListGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: SelectModCategoryPanelView
    ) {
        new TextBlock('Modifier Categories', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.modCategories = new ListGroup(this.view.modCategories);
        this.modCategories.itemClicked.register(this.onModCategoryClicked.bind(this));
        new Command(this.back.bind(this)).add(this.view.backButton);
    }

    private back() {
        this.awaitable.resolve(SelectModCategoryPanelResult.back());
    }

    private onModCategoryClicked(item: ModCategoryListItem) {
        this.awaitable.resolve(
            SelectModCategoryPanelResult.modCategorySelected(item.modCategory)
        );
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let modCategories = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.App.GetModifierCategories()
        );
        modCategories = new FilteredArray(
            modCategories,
            mc => mc.Name.toLowerCase() !== 'default'
        ).value();
        if (modCategories.length === 0) {
            this.awaitable.resolve(SelectModCategoryPanelResult.defaultModSelected());
        }
        else {
            this.modCategories.setItems(
                modCategories,
                (mc: IModifierCategoryModel, itemView: ModCategoryButtonListItemView) =>
                    new ModCategoryListItem(mc, itemView)
            );
        }
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}