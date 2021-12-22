import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";

interface Results {
    defaultModSelected?: {};
    modCategorySelected?: {};
}

export class SelectModCategoryPanelResult {
    static defaultModSelected() {
        return new SelectModCategoryPanelResult({ defaultModSelected: {} });
    }

    static modCategorySelected() {
        return new SelectModCategoryPanelResult({ modCategorySelected: {} });
    }

    private constructor(private readonly results: Results) { }

    get defaultModSelected() {
        return this.results.defaultModSelected;
    }

    get modCategorySelected() {
        return this.results.modCategorySelected;
    }
}

export class SelectModCategoryPanel {
    private readonly awaitable = new Awaitable<SelectModCategoryPanelResult>();

    start() {
        return this.awaitable.start();
    }
}