import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MenuComponent } from "@jasonbenfield/sharedwebapp/Components/MenuComponent";
import { HubAppClient } from "../Lib/Http/HubAppClient";
import { MainMenuPanelView } from "./MainMenuPanelView";

interface IResult {
    back?: boolean;
}

class Result {
    static back() { return new Result({ back: true }); }

    private constructor(private readonly results: IResult) { }

    get back() { return this.results.back; }
}

export class MainMenuPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
            
    constructor(hubClient: HubAppClient, private readonly view: MainMenuPanelView) {
        const menu = new MenuComponent(hubClient, 'main', view.menu);
        menu.refresh();
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
    }

    deactivate() { this.view.hide(); }
}